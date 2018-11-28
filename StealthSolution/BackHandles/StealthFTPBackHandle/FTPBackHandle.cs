﻿using Limilabs.FTP.Client;
using Microsoft.Extensions.Logging;
using SealthModel;
using StealthBackHandle;
using StealthBuildData;
using System;
using System.Text;

namespace StealthFTPBackHandle
{
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



        public FTPBackHandle(ILogger<FTPBackHandle> logger, IBuildData buildData)
        {
            _buildData = buildData;
            _logger = logger;
        }
        public bool Handle(string keyName)
        {
            var content = _buildData.BuildData<string>(keyName);
            return FTPTransfer(content, null);
        }

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
                    var response = ftp.Upload($"{sftpSetting.TransferFilePrefix}", 0, Encoding.UTF8.GetBytes(content));
                    if (response.Code.HasValue && (response.Code.Value == 226 || response.Code.Value == 200))
                    {

                        ftp.Close();
                        _logger.LogInformation($"email send success");
                        return true;
                    }
                    else
                    {
                        _logger.LogError($"ftp failure，because：{response.Message}");
                        ftp.Close();
                        return false;
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, $"ftp send failure:{exc.Message}");

                return false;
            }
        }
    }
}
