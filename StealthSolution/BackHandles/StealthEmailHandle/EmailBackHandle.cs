using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using SealthModel;
using StealthQuartz;
using System;
using System.IO;
using System.Text;

namespace StealthEmailHandle
{
    public class EmailBackHandle : IBackHandle
    {
        readonly ILogger<EmailBackHandle> _logger;
        public EmailBackHandle(ILogger<EmailBackHandle> logger)
        {
            _logger = logger;
        }

        public bool Handle(string content,Encoding encoding, params object[] parmeters)
        {           
            var fileName = parmeters[0].ToString();
            try
            {
                _logger.LogInformation($"email");
                var emailSetting = parmeters[0] as EmailSetting;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(emailSetting.UserName, emailSetting.FromAddresses));
                var toaddresses = emailSetting.ToAddresses?.Split(',');
                foreach (var toaddresse in toaddresses)
                {
                    message.To.Add(new MailboxAddress(toaddresse.Split('@')?[0], toaddresse));
                }
                message.Subject = emailSetting.Subject;
                var builder = new BodyBuilder();
                builder.TextBody = emailSetting.Body;               
                var inStream = new MemoryStream(encoding.GetBytes(content));
                var outStream = new MemoryStream();

                if (emailSetting.IsCompress)
                {
                    outStream = CreateToMemoryStream(inStream, fileName + ".csv", emailSetting.CompressPassword);
                    builder.Attachments.Add($"{fileName}.zip", outStream);
                }
                else
                {
                    builder.Attachments.Add($"{fileName}.csv", inStream);
                }
                message.Body = builder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(emailSetting.Host, emailSetting.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailSetting.FromAddresses, emailSetting.Password);
                    client.Send(message);
                    client.Disconnect(true);
                }
                inStream.Close();
                outStream.Close();

                _logger.LogInformation($"email send fileName:{fileName} success");
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, $"email send fileName:{fileName} failure");
                return false;
            }
           
            MemoryStream CreateToMemoryStream(MemoryStream memStreamIn, string zipEntryName, string compresspassword)
            {
                var outputMemStream = new MemoryStream();
                var zipStream = new ZipOutputStream(outputMemStream);
                zipStream.SetLevel(3); //0-9, 9 being the highest level of compression
                if (!string.IsNullOrEmpty(compresspassword?.Trim()))
                {
                    zipStream.Password = compresspassword;
                }
                var newEntry = new ZipEntry(zipEntryName);
                newEntry.DateTime = DateTime.Now;
                zipStream.PutNextEntry(newEntry);
                var length = memStreamIn.Length < 1024 ? 1024 : memStreamIn.Length;
                StreamUtils.Copy(memStreamIn, zipStream, new byte[length]);
                zipStream.CloseEntry();

                zipStream.IsStreamOwner = false;    // False stops the Close also Closing the underlying stream.
                zipStream.Close();          // Must finish the ZipOutputStream before using outputMemStream.

                outputMemStream.Position = 0;
                return outputMemStream;
            }

        }
    }
}
