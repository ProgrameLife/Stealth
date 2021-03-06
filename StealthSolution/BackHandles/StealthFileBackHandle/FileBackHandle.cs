﻿using Microsoft.Extensions.Logging;
using SealthModel;
using SealthProvider;
using StealthBackHandle;
using StealthBuildData;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StealthFileBackHandle
{
    /// <summary>
    /// email back handle
    /// </summary>
    public class FileBackHandle : IBackHandle
    {
        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<FileBackHandle> _logger;
        /// <summary>
        /// data builder
        /// </summary>
        readonly IBuildData _buildData;
        /// <summary>
        /// email provider
        /// </summary>
        readonly IFileProvider  _fileProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger">dependency injection logger</param>
        /// <param name="buildData">dependency injection  builddate</param>
        /// <param name="fileProvider">dependency injection  file provider</param>
        public FileBackHandle(ILogger<FileBackHandle> logger, IBuildData buildData, IFileProvider fileProvider)
        {
            _buildData = buildData;
            _logger = logger;
            _fileProvider = fileProvider;
        }
        /// <summary>
        /// email handle method
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns></returns>
        public bool Handle(string keyName)
        {
            var content = _buildData.BuildData<string>(keyName);
            var fileSetting = _fileProvider.GetFileSetting(keyName);
            return SendEmail(content, fileSetting);
        }
        /// <summary>
        /// send email
        /// </summary> 
        /// <param name="content">email content</param>
        /// <param name="emailSetting">email setting</param>
        /// <returns></returns>
        bool SendEmail(string content, FileSetting fileSetting)
        {
            try
            {           
                var file = Path.Combine(string.IsNullOrEmpty(fileSetting.FilePath) ? Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) : fileSetting.FilePath, fileSetting.FileName);
                File.WriteAllText(file, content, Encoding.GetEncoding(fileSetting.FileEncoding));
                _logger.LogInformation($"sava file  success");
                return true;         
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, $"sava file  failure");
                return false;
            }  
        }
    }
}
