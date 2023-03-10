using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TabsList.Domain.ViewModel;
using TabsList.Models;
using TabsList.Service.Interfaces;

namespace TabsList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITabsService _tabsService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ITabsService tabsService)
        {
            _logger = logger;
            _tabsService = tabsService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TabsViewModel model)
        {
           var responce = await _tabsService.Create(model);
            if (responce.StatusCode==Domain.Entity.StatusCode.OK)
            {
                return Ok(new { description = responce.Description });
            }
            else
            {
                return BadRequest(new { description = responce.Description });
            }
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