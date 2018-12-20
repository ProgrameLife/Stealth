using Limilabs.FTP.Client;
using Microsoft.Extensions.Logging;
using SealthModel;
using SealthProvider;
using StealthBackHandle;
using StealthBuildData;
using System;
using System.Text;

namespace StealthFTPBackHandle
{
    /// <summary>
    /// ftp back handle
    /// </summary>
    public class FTPBackHandle : IBackHandle
    {
        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<FTPBackHandle> _logger;
        /// <summary>
        /// data builder
        /// </summary>
        readonly IBuildData _buildData;

        /// <summary>
        /// ftp provider
        /// </summary>
        readonly ISFTPProvider _sftpProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger">dependency injection logger</param>
        /// <param name="buildData">dependency injection builddate</param>
        /// <param name="sftpProvider">dependency injection  sftp provider</param>
        public FTPBackHandle(ILogger<FTPBackHandle> logger, IBuildData buildData, ISFTPProvider sftpProvider)
        {
            _buildData = buildData;
            _logger = logger;
            _sftpProvider = sftpProvider;
        }
        /// <summary>
        /// handle ftp
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns></returns>
        public bool Handle(string keyName)
        {
            var content = _buildData.BuildData<string>(keyName);
            var ftpsetting = _sftpProvider.GetSFTPSetting(keyName);
            return FTPTransfer(content, ftpsetting);
        }
        /// <summary>
        /// ftp transfer
        /// </summary>
        /// <param name="content">ftp content</param>
        /// <param name="sftpSetting">ftp setting</param>
        /// <returns></returns>
        bool FTPTransfer(string content, SFTPSetting sftpSetting)
        {
            try
            {
                _logger.LogInformation($"ftp begin");
                using (var ftp = new Ftp())
                {
                    ftp.Connect(sftpSetting.Host);
                    ftp.Login(sftpSetting.UserName, sftpSetting.Password);
                    ftp.Mode = FtpMode.Active;
                    ftp.ChangeFolder(sftpSetting.TransferDirectory);
                    var response = ftp.Upload($"{sftpSetting.TransferFilePrefix}{sftpSetting.FileName}", 0, Encoding.UTF8.GetBytes(content));
                    if (response.Code.HasValue && (response.Code.Value == 226 || response.Code.Value == 200))
                    {

                        ftp.Close();
                        _logger.LogInformation($"ftp uplodad success");
                        return true;
                    }
                    else
                    {
                        _logger.LogError($"ftp uplodad failure，because：{response.Message}");
                        ftp.Close();
                        return false;
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, $"ftp uplodad failure:{exc.Message}");

                return false;
            }
        }
    }
}
