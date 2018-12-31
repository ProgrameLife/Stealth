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
    /// QuartzSettingController
    /// </summary>
    public class QuartzSettingsController : Controller
    {
        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<QuartzSettingsController> _logger;
        /// <summary>
        /// Quartz data provider  
        /// </summary>
        readonly IQuartzProvider _quartzProvider;
        /// <summary>
        /// cotr
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="quartzProvider"></param>
        public QuartzSettingsController(ILogger<QuartzSettingsController> logger, IQuartzProvider quartzProvider)
        {
            _logger = logger;
            _quartzProvider = quartzProvider;
        }
        /// <summary>
        /// index view
        /// </summary>
        /// <returns>Return a view</returns>
        [HttpGet("/quartzsettings")]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// get all Quartzsetting,contains invalidate
        /// </summary>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message,the third is data property, indicating the return data</returns>
        [HttpGet("quartzsettings/{pageindex}")]
        public IActionResult GetAllSetting(int pageindex = 1)
        {
            try
            {
                _logger.LogInformation($"get all Quartzsetting， pageindex={pageindex}");
                var result = _quartzProvider.GetAllQuartzSetting(pageindex);
                return new JsonResult(new { result = true, data = new { list = result.list, total = result.total } });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// add Quartzsetting
        /// </summary>
        /// <param name="QuartzSetting">Quartzsetting</param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpPost("addquartzsetting")]
        public IActionResult AddQuartzSetting([FromBody]QuartzSetting QuartzSetting)
        {
            try
            {
                _logger.LogInformation($"add Quartzsetting,keyname={QuartzSetting.KeyName}");
                return new JsonResult(new { result = true, data = _quartzProvider.AddQuartzSetting(QuartzSetting) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// modify Quartzsetting
        /// </summary>
        /// <param name="QuartzSetting"></param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpPut("modifyquartzsetting")]
        public IActionResult ModifyQuartzSetting([FromBody] QuartzSetting QuartzSetting)
        {
            try
            {
                _logger.LogInformation($"modify Quartzsetting,keyname={QuartzSetting.KeyName}");
                return new JsonResult(new { result = true, data = _quartzProvider.ModifyQuartzSetting(QuartzSetting) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// remove Quartzsetting
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpDelete("removequartzsetting/{id}")]
        public IActionResult RemoveQuartzSetting(int id)
        {
            try
            {
                _logger.LogInformation($"remove Quartzsetting,id={id}");
                return new JsonResult(new { result = true, data = _quartzProvider.RemoveQuartzSetting(id) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
    }
}