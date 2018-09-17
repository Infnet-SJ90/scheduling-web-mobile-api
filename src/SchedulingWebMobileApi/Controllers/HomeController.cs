using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulingWebMobileApi.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var content = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/View/index.html");
            return new ContentResult()
            {
                Content = content,
                ContentType = "text/plain",
            };
        }
    }
}
