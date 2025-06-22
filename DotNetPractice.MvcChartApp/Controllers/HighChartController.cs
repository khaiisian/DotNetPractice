using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.MvcChartApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
