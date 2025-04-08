using DotNetPractice.PizzaApi.Db;
using DotNetPractice.PizzaApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.PizzaApi.Controllers
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

        [HttpGet("Pizzas")]
        public async Task<IActionResult> GetPizzasAsync()
        {
            List<PizzaModel> lst = await _context.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtrasAsync()
        {
            List<ExtraModel> lst = await _context.Extras.ToListAsync();
            return Ok(lst);
        }

        [HttpPost]
        public async Task<IActionResult> OrderAsync(OrderRequest requestModel)
        {
            var item = await _context.Pizzas.FirstOrDefaultAsync(x=>x.Pizza_Id == requestModel.Pizza_Id);
            if (item == null) return NotFound("no data found");
            var totalAmount = item.Price;

            string[] extra_names = null;
            if(requestModel.Extras_Id.Length> 0)
            {
                List<ExtraModel> extraLst = await _context.Extras.Where(x => requestModel.Extras_Id.Contains(x.Extra_Id)).ToListAsync();
                var extraAmount = extraLst.Sum(x=>x.Price);
                totalAmount += extraAmount;
                extra_names = extraLst.Select(x=>x.Extra_Name).ToArray();
            }

            var invoice_num = DateTime.Now.ToString("yyyyMMddHHmmss");

            OrderModel orderModel = new OrderModel()
            {
                Invoice_Num = invoice_num,
                Pizza_Id = requestModel.Pizza_Id,
                Total_Amount = totalAmount,
            };

            await _context.Orders.AddAsync(orderModel);
            int orderResult = await _context.SaveChangesAsync();
            if (orderResult == 0) return BadRequest("Order Failed");

            List<OrderDetailModel> orderDetailModels = requestModel.Extras_Id.Select(x=> new OrderDetailModel()
            {
                Extra_Id = x,
                Order_Id = orderModel.Order_Id,
            }).ToList();

            await _context.OrderDetails.AddRangeAsync(orderDetailModels);
            await _context.SaveChangesAsync();

            OrderResponse orderResponse = new OrderResponse()
            {
                Invoice_Number = invoice_num,
                Pizza_Name = item.Pizza_Name,
                Extras = extra_names,
                Total_Amount = $"$ {totalAmount}",
            };

            return Ok(orderResponse);
        }

        [HttpGet("Orders/{invoice_num}")]
        public async Task<IActionResult> GetOrderAsync(string invoice_num)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x=>x.Invoice_Num == invoice_num);
            if (order == null) return NotFound("No data found");
            var detailExtra = await _context.OrderDetails.Where(x=>x.Order_Id == order.Order_Id).Select(x=>x.Extra_Id).ToListAsync();
            var extraLst = await _context.Extras.Where(x=>detailExtra.Contains(x.Extra_Id)).Select(x=>x.Extra_Name).ToListAsync();
            string[] extra_names = extraLst.ToArray();

            var Pizza = await _context.Pizzas.FirstOrDefaultAsync(x => x.Pizza_Id == order.Pizza_Id);
            
            OrderResponse orderResponse = new OrderResponse()
            {
                Invoice_Number = invoice_num,
                Pizza_Name = Pizza.Pizza_Name,
                Extras = extra_names,
                Total_Amount = $"$ {order.Total_Amount}",
            }; 
            return Ok(orderResponse);
        }
    }
}
