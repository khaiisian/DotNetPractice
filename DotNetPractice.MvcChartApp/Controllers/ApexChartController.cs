using DotNetPractice.MvcChartApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.MvcChartApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            List<int> series = new List<int>() {30, 40, 20, 10};
            List<string> labels = new List<string> { "Team A", "Team B", "Team C", "Team D" };
            PieChartModel model = new PieChartModel()
            {
                series = series,
                labels = labels,
            };
            return View(model);
        }
    }
}
