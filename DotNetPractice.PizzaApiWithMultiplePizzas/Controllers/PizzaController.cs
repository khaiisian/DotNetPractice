using DotNetPractice.PizzaApiWithMultiplePizzas.Db;
using DotNetPractice.PizzaApiWithMultiplePizzas.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DotNetPractice.PizzaApiWithMultiplePizzas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PizzaController()
        {
            _appDbContext = new AppDbContext();
        }

        [HttpGet("Pizza")]
        public async Task<IActionResult> PizzasAsync()
        {
            var lst = await _appDbContext.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> ExtrasAsync()
        {
            var lst = await _appDbContext.Extras.ToListAsync();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult Order(OrderRequest requestModel)
        {
            decimal totalAmount = 0;

            foreach (var item in requestModel.OrderItems)
            {
                var pizza = _appDbContext.Pizzas.FirstOrDefault(x => x.PizzaId == item.PizzaId);
                if (pizza is null) return BadRequest("You Pizza is not found");

                if(item.Extras != null && item.Extras.Length > 0)
                {
                    var extra_ids = item.Extras;
                    var extras = _appDbContext.Extras.Where(x=>extra_ids.Contains(x.ExtraId)).ToList();
                    if(item.Quantity > 0)
                    {
                        var subtotal = item.Quantity * (pizza.Price + extras.Sum(x => x.Price));
                        totalAmount += subtotal;
                    }
                }

            }

            //var pizza_ids = requestModel.OrderItems.Select(x => x.PizzaId).ToList();
            //List<int> extra_ids = new List<int>();

            //var pizzas = _appDbContext.Pizzas.Where(x => pizza_ids.Contains(x.PizzaId)).ToList();
            //var total_amount = pizzas.Sum(x=>x.Price);

            //foreach(var item in requestModel.OrderItems)
            //{
            //    if(item.Extras!=null && item.Extras.Length> 0)
            //    {
            //        foreach(var extra in item.Extras)
            //        {
            //            extra_ids.Add(extra);
            //        }
            //    }
            //}

            //if(extra_ids.Count>0)
            //{
            //    var extras = _appDbContext.Extras.Where(x=>extra_ids.Contains(x.ExtraId)).ToList();
            //    total_amount += extras.Sum(x => x.Price);
            //}

            string Invoice_num = DateTime.Now.ToString("yyyyMMddHHmmss");
            OrderModel order = new OrderModel()
            {
                InvoiceNum = Invoice_num,
                TotalAmount = totalAmount,
            };

            _appDbContext.Orders.Add(order);
            int orderResult = _appDbContext.SaveChanges();
            if (orderResult == 0) return BadRequest("Order Failed");

            List<OrderItemModel> orderitems = requestModel.OrderItems.Select(x => new OrderItemModel()
            {
                OrderId = order.OrderId,
                PizzaId = x.PizzaId,
                Quantity = x.Quantity,
            }).ToList();

            _appDbContext.OrderItems.AddRange(orderitems);
            int orderItemResult = _appDbContext.SaveChanges();
            if (orderItemResult == 0) return BadRequest("Order Item Failure");

            var orderItemIds = orderitems.Select(x => x.OrderItemId).ToArray();

            var extraIdLst = requestModel.OrderItems.Select(x => x.Extras.ToList()).ToList();
            
            int index = 0;
            foreach (var item in extraIdLst)
            {
                var lst = item.Select(x=>new OrderDetailModel
                {
                    OrderItemId = orderItemIds[index],
                    ExtraId = x
                }).ToList();

                _appDbContext.OrderDetails.AddRange(lst);
                index++;
            }

            _appDbContext.SaveChanges();

            return Ok("Order Successful");
        }



    }
}
