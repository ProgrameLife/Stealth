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
    /// EmailSettingController
    /// </summary>
    public class EmailSettingsController : Controller
    {
        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<EmailSettingsController> _logger;
        /// <summary>
        /// email data provider  
        /// </summary>
        readonly IEmailProvider _emailProvider;
        /// <summary>
        /// cotr
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="emailProvider"></param>
        public EmailSettingsController(ILogger<EmailSettingsController> logger, IEmailProvider emailProvider)
        {
            _logger = logger;
            _emailProvider = emailProvider;
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
        /// get all emailsetting,contains invalidate
        /// </summary>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message,the third is data property, indicating the return data</returns>
        [HttpGet("emailsettings/{pageindex}")]
        public IActionResult GetAllSetting(int pageindex = 1)
        {
            try
            {
                _logger.LogInformation($"get all emailsetting， pageindex={pageindex}");
                var result = _emailProvider.GetAllEmailSetting(pageindex);
                return new JsonResult(new { result = true, data = new { list = result.list, total = result.total } });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// add emailsetting
        /// </summary>
        /// <param name="emailSetting">emailsetting</param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpPost("addemailsetting")]
        public IActionResult AddEmailSetting([FromBody]EmailSetting emailSetting)
        {
            try
            {
                _logger.LogInformation($"add emailsetting,keyname={emailSetting.KeyName}");
                return new JsonResult(new { result = true, data = _emailProvider.AddEmailSetting(emailSetting) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// modify emailsetting
        /// </summary>
        /// <param name="emailSetting"></param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpPut("modifyemailsetting")]
        public IActionResult ModifyEmailSetting([FromBody] EmailSetting emailSetting)
        {
            try
            {
                _logger.LogInformation($"modify emailsetting,keyname={emailSetting.KeyName}");
                return new JsonResult(new { result = true, data = _emailProvider.ModifyEmailSetting(emailSetting) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
        /// <summary>
        /// remove emailsetting
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return json object, one property is result, of Boolean type, indicating whether the return is successful, and the other is message, indicating the error message</returns>
        [HttpDelete("removeemailsetting/{id}")]
        public IActionResult RemoveEmailSetting(int id)
        {
            try
            {
                _logger.LogInformation($"remove emailsetting,id={id}");
                return new JsonResult(new { result = true, data = _emailProvider.RemoveEmailSetting(id) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }
    }
}