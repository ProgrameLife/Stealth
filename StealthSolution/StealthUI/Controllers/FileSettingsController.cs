using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SealthModel;
using SealthProvider;

namespace StealthUI.Controllers
{
    /// <summary>
    /// FileSettingController
    /// </summary>
    public class FileSettingsController : Controller
    {
        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<FileSettingsController> _logger;
        /// <summary>
        /// File data provider  
        /// </summary>
        readonly IFileProvider _fileProvider;
        /// <summary>
        /// cotr
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="fileProvider"></param>
        public FileSettingsController(ILogger<FileSettingsController> logger, IFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
        }
        /// <summary>
        /// index view
        /// </summary>
        /// <returns>Return a view</returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// get all Filesetting,contains invalidate
        /// </summary>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message,the third is data property, indicating the return data</returns>
        [HttpGet("Filesettings")]
        public IActionResult GetAllSetting()
        {
            try
            {
                _logger.LogInformation("get all Filesetting");
                return new JsonResult(new { result = true, data = _fileProvider.GetAllFileSetting() });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// add Filesetting
        /// </summary>
        /// <param name="FileSetting">Filesetting</param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpPost("addFilesetting")]
        public IActionResult AddFileSetting(FileSetting FileSetting)
        {
            try
            {
                _logger.LogInformation($"add Filesetting,keyname={FileSetting.KeyName}");
                return new JsonResult(new { result = true, data = _fileProvider.AddFileSetting(FileSetting) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// modify Filesetting
        /// </summary>
        /// <param name="FileSetting"></param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpPut("modifyFilesetting")]
        public IActionResult ModifyFileSetting(FileSetting FileSetting)
        {
            try
            {
                _logger.LogInformation($"modify Filesetting,keyname={FileSetting.KeyName}");
                return new JsonResult(new { result = true, data = _fileProvider.ModifyFileSetting(FileSetting) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// remove Filesetting
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpDelete("removeFilesetting/{id}")]
        public IActionResult RemoveFileSetting(int id)
        {
            try
            {
                _logger.LogInformation($"remove Filesetting,id={id}");
                return new JsonResult(new { result = true, data = _fileProvider.RemoveFileSetting(id) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
    }
}