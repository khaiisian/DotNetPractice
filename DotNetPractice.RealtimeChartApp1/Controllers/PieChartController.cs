using DotNetPractice.RealtimeChartApp1.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.RealtimeChartApp1.Controllers
{
    public class PieChartController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public PieChartController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PieChart()
        {
            var model = _appDbContext.TblPieCharts
                .Select(x => new PieChartModel
                {
                    name = x.PieChartName,
                    y = x.PieChartValue,
                })
                .ToList();
            return View(model);
        }

        public IActionResult CreatePieChart()
        {
            return View();
        }

        public IActionResult Save(TblPieChart requestModel)
        {
            _appDbContext.Add(requestModel);
            _appDbContext.SaveChanges();
            return Redirect("CreatePieChart");
        }
    }

    public class PieChartModel
    {
        public string? name { get; set; }
        public decimal y { get; set; }
    }
}
