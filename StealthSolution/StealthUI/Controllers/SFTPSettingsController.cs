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
    /// SFTPSettingController
    /// </summary>
    public class SFTPSettingsController : Controller
    {
        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<SFTPSettingsController> _logger;
        /// <summary>
        /// SFTP data provider  
        /// </summary>
        readonly ISFTPProvider _SFTPProvider;
        /// <summary>
        /// cotr
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="SFTPProvider"></param>
        public SFTPSettingsController(ILogger<SFTPSettingsController> logger, ISFTPProvider SFTPProvider)
        {
            _logger = logger;
            _SFTPProvider = SFTPProvider;
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
        /// get all SFTPsetting,contains invalidate
        /// </summary>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message,the third is data property, indicating the return data</returns>
        [HttpGet("SFTPsettings/{pageIndex}")]
        public IActionResult GetAllSetting(int pageIndex=1)
        {
            try
            {
                _logger.LogInformation("get all SFTPsetting");
                var result = _SFTPProvider.GetAllSFTPSetting(pageIndex);
                return new JsonResult(new { result = true, data = new { list = result.list, total = result.total } });        
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// add SFTPsetting
        /// </summary>
        /// <param name="SFTPSetting">SFTPsetting</param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpPost("addSFTPsetting")]
        public IActionResult AddSFTPSetting(SFTPSetting SFTPSetting)
        {
            try
            {
                _logger.LogInformation($"add SFTPsetting,keyname={SFTPSetting.KeyName}");
                return new JsonResult(new { result = true, data = _SFTPProvider.AddSFTPSetting(SFTPSetting) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// modify SFTPsetting
        /// </summary>
        /// <param name="SFTPSetting"></param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpPut("modifySFTPsetting")]
        public IActionResult ModifySFTPSetting(SFTPSetting SFTPSetting)
        {
            try
            {
                _logger.LogInformation($"modify SFTPsetting,keyname={SFTPSetting.KeyName}");
                return new JsonResult(new { result = true, data = _SFTPProvider.ModifySFTPSetting(SFTPSetting) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// remove SFTPsetting
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpDelete("removeSFTPsetting/{id}")]
        public IActionResult RemoveSFTPSetting(int id)
        {
            try
            {
                _logger.LogInformation($"remove SFTPsetting,id={id}");
                return new JsonResult(new { result = true, data = _SFTPProvider.RemoveSFTPSetting(id) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
    }
}