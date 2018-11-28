using Microsoft.Extensions.Logging;
using Renci.SshNet;
using SealthModel;
using SealthProvider;
using StealthBackHandle;
using StealthBuildData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StealthSFTPBackHandle
{
    public class SFTPBackHandle : IBackHandle
    {
        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<SFTPBackHandle> _logger;
        /// <summary>
        /// data builder
        /// </summary>
        readonly IBuildData _buildData;

        /// <summary>
        /// ftp provider
        /// </summary>
        readonly ISFTPProvider _sftpProvider;
        public SFTPBackHandle(ILogger<SFTPBackHandle> logger, IBuildData buildData, ISFTPProvider sftpProvider)
        {
            _buildData = buildData;
            _logger = logger;
            _sftpProvider = sftpProvider;
        }
        public bool Handle(string keyName)
        {
            var content = _buildData.BuildData<string>(keyName);
            var ftpsetting = _sftpProvider.GetSFTPSetting(keyName);
            return SFTPTransfer(content, Encoding.UTF8, ftpsetting);
        }


        bool SFTPTransfer(string content, Encoding encoding, SFTPSetting sftpSetting)
        {
            _logger.LogInformation("sftp begin");


            var authMethods = new List<AuthenticationMethod>();
            if (!string.IsNullOrEmpty(sftpSetting.Password?.Trim()))
            {
                authMethods.Add(new PasswordAuthenticationMethod(sftpSetting.UserName, sftpSetting.Password));
            }
            if (!string.IsNullOrEmpty(sftpSetting?.CertificatePath?.Trim()))
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                authMethods.Add(new PrivateKeyAuthenticationMethod(sftpSetting.UserName, new PrivateKeyFile(pathToContentRoot + sftpSetting.CertificatePath)));
            }
            var connectionInfo = new ConnectionInfo(sftpSetting.Host, sftpSetting.Port,
                                        sftpSetting.UserName,
                                        authMethods.ToArray()
                                        );
            //todo 这里没有作长连接，因为用户量较小，用户量较大时，可以设置长连接
            using (var client = new SftpClient(connectionInfo))
            {
                client.Connect();
                //创建用户保存文件夹,并且进入文件夹下
                var dirArr = sftpSetting.TransferDirectory.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                var mark = true;
                foreach (var dir in dirArr)
                {
                    if (!client.Exists(dir) && mark)
                    {
                        client.CreateDirectory(dir);
                    }
                    else
                    {
                        mark = false;
                    }
                    client.ChangeDirectory(dir);
                }
                var attachmentArry = encoding.GetBytes(content);
                var stream = new MemoryStream(attachmentArry);
                try
                {
                    client.UploadFile(stream, $"{sftpSetting.TransferFilePrefix}{sftpSetting.FileName}", (result) =>
                    {
                        if (result < 0)
                        {
                            _logger.LogError($"sftp failure:result={result}");
                        }
                    });
                }
                catch (Exception exc)
                {
                    _logger.LogCritical(exc, $"sftp failure:{exc.Message}");
                    return false;
                }
            }
            _logger.LogInformation($"sftp success");
            return true;

        }
    }
}
