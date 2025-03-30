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

        [HttpPost()]
        public async Task<IActionResult> orderAsync(OrderRequest requestModel)
        {
            var itemPizza = await _context.Pizzas.FirstOrDefaultAsync(x => x.Pizza_Id == requestModel.PizzaId);
            if (itemPizza == null)
            {
                return NotFound("no pizza found");
            }
            var totalAmount = itemPizza.Price;

            string[] extra_pizzas = null;
            if (requestModel.Extra is not null && requestModel.Extra.Length > 0)
            {
                var extraLst = await _context.Extras.Where(x => requestModel.Extra.Contains(x.Extra_Id)).ToListAsync();
                var extra_amount = extraLst.Sum(x => x.Price);
                totalAmount += extra_amount;

                extra_pizzas = extraLst.Select(x => x.Extra_Name).ToArray();
            }

            var invoiceNum = DateTime.Now.ToString("yyyyMMddHHmmss");
            OrderModel orderModel = new OrderModel()
            {
                Pizza_Id = requestModel.PizzaId,
                Invoice_Num = invoiceNum,
                Total_Amount = totalAmount,
            };
            await _context.Order.AddAsync(orderModel);
            int orderResult = await _context.SaveChangesAsync();

            List<OrderDetailModel> orderDetailLst = requestModel.Extra.Select(x => new OrderDetailModel()
            {
                Extra_Id = x,
                Order_Id = orderModel.Order_Id,
            }).ToList();
            await _context.OrderDetail.AddRangeAsync(orderDetailLst);
            int orderDetailResult = await _context.SaveChangesAsync();

            if(orderResult == 0 )
            {
                return BadRequest("Order Failed");
            }

            OrderResponse orderResponse = new OrderResponse()
            {
                Message = "Your Order is Successful",
                Order_Invoice = invoiceNum,
                Pizza = itemPizza.Pizza_Name,
                Total_AmountString = $"$ {totalAmount}",
            };
            if(extra_pizzas != null && extra_pizzas.Length > 0)
            {
                orderResponse.Extra = extra_pizzas;
            }

            return Ok(orderResponse);
        }
    }
}
