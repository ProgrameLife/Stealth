using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
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

        public bool Handle(params object[] parmeters)
        {
            throw new NotImplementedException();
            //try
            //{
            //    _logger.LogInformation($"email");
            //    var db = new Npgsql.NpgsqlConnection(transferParmeter.ConnectionString);
            //    var emailSettings = db.Query<dynamic>("select * from settingemails where transferid=@transferid", new { transferParmeter.TransferID });
            //    foreach (var emailSetting in emailSettings)
            //    {
            //        var message = new MimeMessage();
            //        message.From.Add(new MailboxAddress(emailSetting.username, emailSetting.fromaddress));
            //        var toaddresses = emailSetting.toaddresses?.Split(',');
            //        foreach (var toaddresse in toaddresses)
            //        {
            //            message.To.Add(new MailboxAddress(toaddresse.Split('@')?[0], toaddresse));
            //        }
            //        message.Subject = emailSetting.subject;
            //        var builder = new BodyBuilder();
            //        builder.TextBody = emailSetting.body;

            //        // var sjisEnc = Encoding.GetEncoding("Shift_JIS");  
            //        var sjisEnc = Encoding.UTF8;
            //        var inStream = new MemoryStream(sjisEnc.GetBytes(transferParmeter.Content));
            //        var outStream = new MemoryStream();
            //        if (emailSetting.iscompress)
            //        {
            //            outStream = CreateToMemoryStream(inStream, transferParmeter.FileName + ".csv", emailSetting.compresspassword);
            //            builder.Attachments.Add($"{transferParmeter.FileName}.zip", outStream);
            //        }
            //        else
            //        {
            //            builder.Attachments.Add($"{transferParmeter.FileName}.csv", inStream);
            //        }
            //        message.Body = builder.ToMessageBody();
            //        using (var client = new SmtpClient())
            //        {
            //            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            //            client.Connect(emailSetting.host, emailSetting.port, true);
            //            client.AuthenticationMechanisms.Remove("XOAUTH2");
            //            client.Authenticate(emailSetting.fromaddress, emailSetting.password);
            //            client.Send(message);
            //            client.Disconnect(true);
            //        }
            //        inStream.Close();
            //        outStream.Close();
            //    }
            //    _logger.LogInformation($"email上传文件transferid：{transferParmeter.TransferID}，fileName:{transferParmeter.FileName}成功");
            //    return true;
            //}
            //catch (Exception exc)
            //{
            //    _logger.LogCritical(exc, $"email上传文件transferid：{transferParmeter.TransferID}，fileName:{transferParmeter.FileName}失败");
            //    return false;
            //}
            ////内部方法
            //MemoryStream CreateToMemoryStream(MemoryStream memStreamIn, string zipEntryName, string compresspassword)
            //{
            //    var outputMemStream = new MemoryStream();
            //    var zipStream = new ZipOutputStream(outputMemStream);
            //    zipStream.SetLevel(3); //0-9, 9 being the highest level of compression
            //    if (!string.IsNullOrEmpty(compresspassword?.Trim()))
            //    {
            //        zipStream.Password = compresspassword;
            //    }
            //    var newEntry = new ZipEntry(zipEntryName);
            //    newEntry.DateTime = DateTime.Now;
            //    zipStream.PutNextEntry(newEntry);
            //    var length = memStreamIn.Length < 1024 ? 1024 : memStreamIn.Length;
            //    StreamUtils.Copy(memStreamIn, zipStream, new byte[length]);
            //    zipStream.CloseEntry();

            //    zipStream.IsStreamOwner = false;    // False stops the Close also Closing the underlying stream.
            //    zipStream.Close();          // Must finish the ZipOutputStream before using outputMemStream.

            //    outputMemStream.Position = 0;
            //    return outputMemStream;
            //}

        }
    }
}
