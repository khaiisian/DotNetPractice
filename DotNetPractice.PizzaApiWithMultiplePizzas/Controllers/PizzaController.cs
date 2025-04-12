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

            var resOrder = _appDbContext.Orders.FirstOrDefault(x => x.InvoiceNum == Invoice_num);
            if (resOrder == null) return NotFound("No order to return");
            
            var resOrderItems = _appDbContext.OrderItems.Where(x => x.OrderId == resOrder!.OrderId).ToList();

            var orderItemIdList = _= resOrderItems.Select(x=>x.OrderItemId).ToList();
            var resOrderExtras = _appDbContext.OrderDetails.Where(x=> orderItemIdList.Contains(x.OrderItemId)).ToList();

            var response_extraLst = resOrderExtras.Select(x =>
            {
                var extra = _appDbContext.Extras.FirstOrDefault(a => a.ExtraId == x.ExtraId);
                return new OrderResExtra
                {
                    ExtraName = extra!.ExtraName,
                    ExtraAmount = $"${extra!.Price}"
                };
            }).ToList();

            var response_orderItemlst = resOrderItems.Select(x =>
            {
                var pizza = _appDbContext.Pizzas.FirstOrDefault(a => a.PizzaId == x.PizzaId);
                return new OrderResItem
                {
                    PizzaName = pizza!.PizzaName,
                    PizzaAmount = $"${pizza!.Price}",
                    OrderResExtra = response_extraLst
                };
            }).ToList();

            var respnesOrder = new OrderResponse
            {
                InvoiceNum = resOrder.InvoiceNum,
                OrderResItems = response_orderItemlst,
                TotalAmount = $"{resOrder.TotalAmount}"
            };

            return Ok(respnesOrder);
        }



    }
}
