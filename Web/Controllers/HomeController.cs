using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidFireLib.Lib.Core;
using RapidFireLib.View.UserInfo;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
       
        RapidFire rf;
        public HomeController(IConfig config, IUserInfo userInfo)
        {
            rf = new RapidFire(config);
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
