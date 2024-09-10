using Microsoft.AspNetCore.Mvc;
using Quorum.TestCaseGenerator.Models;
using Quorum.TestCaseGenerator.Service.Services;
using System.Diagnostics;

namespace Quorum.TestCaseGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeService _homeService;
        public HomeController(ILogger<HomeController> logger , HomeService service)
        {
            _logger = logger;
            _homeService = service;
        }

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

        [HttpPost]
        public async Task<JsonResult> GenerateReport()
        {
            bool isSuccess = true;// Your action logic here (e.g., database update or some operation)
            await _homeService.ExportExcel();

            return Json(new { success = isSuccess });
        }
    }
}