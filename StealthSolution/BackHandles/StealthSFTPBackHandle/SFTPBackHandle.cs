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
    /// <summary>
    /// sftp back handle
    /// </summary>
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
        /// <summary>
        /// sftp ctor
        /// </summary>
        /// <param name="logger">dependency injection  logger</param>
        /// <param name="buildData">dependency injection builddate </param>
        /// <param name="sftpProvider">dependency injection sftp provider</param>
        public SFTPBackHandle(ILogger<SFTPBackHandle> logger, IBuildData buildData, ISFTPProvider sftpProvider)
        {
            _buildData = buildData;
            _logger = logger;
            _sftpProvider = sftpProvider;
        }
        /// <summary>
        /// handle sftp
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns></returns>
        public bool Handle(string keyName)
        {
            var content = _buildData.BuildData<string>(keyName);
            var ftpsetting = _sftpProvider.GetSFTPSetting(keyName);
            return SFTPTransfer(content, ftpsetting);
        }

        /// <summary>
        /// sftp transfer
        /// </summary>
        /// <param name="content">sftp content</param>
        /// <param name="sftpSetting">sftpsetting</param>
        /// <returns></returns>
        bool SFTPTransfer(string content, SFTPSetting sftpSetting)
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
            using (var client = new SftpClient(connectionInfo))
            {
                client.Connect();
                //create directory and enter this directory
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
                var encoding = Encoding.GetEncoding(sftpSetting.FileEncoding);
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
