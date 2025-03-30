using DotNetPractice.RestApiWithNLayer.Db;
using DotNetPractice.RestApiWithNLayer.Models.PizzaModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.RestApiWithNLayer.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PizzaController()
        {
            _context = new AppDbContext();
        }

        [HttpGet("Pizza")]
        public async Task<IActionResult> GetPizzaAsync()
        {
            List<PizzaModel> lst = await _context.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extra")]
        public async Task<IActionResult> getExtrasAsync()
        {
            List<ExtraModel> lst = await _context.Extras.ToListAsync();
            return Ok(lst);
        }
    }
}
