using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.MvcChartApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InterpolationChart()
        {
            return View();
        }
    }
}
