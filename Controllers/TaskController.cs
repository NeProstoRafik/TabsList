using Microsoft.AspNetCore.Mvc;
using TabsList.Domain.ViewModel;

namespace TabsList.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TabsViewModel model)
        {
            return Ok();
        }
    }
}
