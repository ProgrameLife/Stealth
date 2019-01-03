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
    public class StealthStatuController : Controller
    {
        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<StealthStatuController> _logger;
        /// <summary>
        /// email data provider  
        /// </summary>
        readonly IStealthStatuProvider  _stealthStatuProvider;
        /// <summary>
        /// cotr
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="emailProvider"></param>
        public StealthStatuController(ILogger<StealthStatuController> logger, IStealthStatuProvider   stealthStatuProvider)
        {
            _logger = logger;
            _stealthStatuProvider = stealthStatuProvider;
        }
        /// <summary>
        /// index view
        /// </summary>
        /// <returns>Return a view</returns>
        [HttpGet("/stealthstatu")]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// get all emailsetting,contains invalidate
        /// </summary>
        /// <returns>Return json object</returns>
        [HttpGet("getstealthstatu")]
        public IActionResult GetAllSetting(int pageindex = 1)
        {
            try
            {
                _logger.LogInformation($"get all stealthstatu");            
                return new JsonResult(new { result = true, data = _stealthStatuProvider.GetAllStealthsStatus() });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { result = false, message = exc.Message });
            }
        }       
    }
}