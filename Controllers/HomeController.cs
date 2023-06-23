using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TabsList.DAL;
using TabsList.Domain.Entity;
using TabsList.Domain.ViewModel;
using TabsList.Models;
using TabsList.Service.Interfaces;

namespace TabsList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITabsService _tabsService;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext applicationDb;

        public HomeController(ILogger<HomeController> logger, ITabsService tabsService)
        {
            _logger = logger;
            _tabsService = tabsService;
           
        }
        [HttpGet]
        public ActionResult Index()
        {
            var result = _tabsService.GetAll().Result;
            return View(result);          
        }

        //[HttpPut("{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var responce = await _tabsService.Delete(id);
            if (responce.StatusCode == Domain.Entity.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        //public async Task<ActionResult> TabsHandler()
        //{
        // var result = _tabsService.GetAll();
           
        //    return View(result);
        //}
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
        public IActionResult Edit(int id)
        {
            var res = _tabsService.Get(id);
       
            return View(res);
       
        }

        public async Task<IActionResult> Update(TabsViewModel model)
        {
            var responce = await _tabsService.Update(model);
            return Ok(new { description = responce.Description });
            // хз чтото не робит
        //    if (responce.StatusCode == Domain.Entity.StatusCode.OK)
        //    {
        //      //  await Ok(new { description = responce.Description });

        //        return RedirectToAction("TabsHandler", "Home");
        //    }
        //   return RedirectToAction("Error");
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