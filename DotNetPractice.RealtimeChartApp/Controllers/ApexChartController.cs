using DotNetPractice.RealtimeChartApp.Hubs;
using DotNetPractice.RealtimeChartApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DotNetPractice.RealtimeChartApp.Controllers
{
    public class ApexChartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChartHubs> _hubContext;

        public ApexChartController(AppDbContext context, IHubContext<ChartHubs> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PieChart()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Save(TblPieChart reqModel)
        {
            _context.TblPieCharts.Add(reqModel);
            int result = _context.SaveChanges();
            var data = _context.TblPieCharts.Select(x=>new PieChartData
            {
                name = x.PieChartName,
                y = x.PieChartValue,
            }).ToList();
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", data);
            return Redirect("Create");
        }
    }

    public class PieChartData
    {
        public string? name { get; set; }
        public decimal y { get; set; }
    }
}
