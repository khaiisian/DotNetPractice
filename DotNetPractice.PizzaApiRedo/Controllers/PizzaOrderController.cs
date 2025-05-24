using DotNetPractice.PizzaApiRedo.Db;
using DotNetPractice.PizzaApiRedo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.PizzaApiRedo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaOrderController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PizzaOrderController()
        {
            _appDbContext = new AppDbContext();
        }

        [HttpGet("Pizzas")]
        public async Task<IActionResult> getPizzasAsync()
        {
            List<PizzaModel> pizzaList = await _appDbContext.Pizzas.ToListAsync();
            return Ok(pizzaList);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> getExtras ()
        {
            List<ExtraModel> extraList = await _appDbContext.Extras.ToListAsync();
            return Ok(extraList);
        }

        [HttpPost("Orders")]
        public IActionResult orderPizza(OrderRequestModel requestModel)
        {
            decimal totalAmount = 0;
            foreach(var item in requestModel.OrderItems)
            {

            }
        }
    }
}
