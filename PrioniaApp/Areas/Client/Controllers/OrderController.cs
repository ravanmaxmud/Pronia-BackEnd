﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.OrderProducts;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("order")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public OrderController(DataContext dbContext, IFileService fileService, IUserService userService, IOrderService orderService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _userService = userService;
            _orderService = orderService;
        }

        [HttpGet("checkout",Name ="client-order-checkout")]
        public async Task<IActionResult> CheckOut()
        {
            var model = new OrderProductsViewModel
            {
                Products = await _dbContext.BasketProducts
                .Where(p=> p.Basket.UserId == _userService.CurrentUser.Id)
                  .Select(p=> new OrderProductsViewModel.ListItem 
                  {
                     Name = p.Product.Name,
                     Price = p.Product.Price,
                     Quantity = p.Quantity,
                     Total = p.Product.Price * p.Quantity,
                  }).ToListAsync(),

                Summary = new OrderProductsViewModel.SummaryTotal 
                {
                   Total = await _dbContext.BasketProducts
                   .Where(pu=> pu.Basket.UserId == _userService.CurrentUser.Id)
                    .SumAsync(bp=> bp.Product.Price * bp.Quantity)
                }
            };

            return View(model);
        }


        [HttpPost("placeorder", Name = "client-order-placeorder")]
        public async Task<IActionResult> PlaceOrder()
        {
            var basketProducts = await _dbContext.BasketProducts.Include(p => p.Product)
                .Where(p => p.Basket.UserId == _userService.CurrentUser.Id).ToListAsync();

            var order = await CreateOrder();

            await CreateFullOrderProductAync(order, basketProducts);
            order.SumTotalPrice = order.OrderProducts.Sum(p=> p.Total);

            await ResetBasketAsync(basketProducts);

            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-account-order");


            async Task ResetBasketAsync(List<BasketProduct> basketProducts)
            {
                await Task.Run(() => _dbContext.RemoveRange(basketProducts));
            }

            async Task CreateFullOrderProductAync(Order order, List<BasketProduct> basketProducts)
            {
                foreach (var item in basketProducts)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = item.Product.Price,
                        Quantity = item.Quantity,
                        Total = item.Product.Price * item.Quantity,
                    };
                    await _dbContext.OrderProducts.AddAsync(orderProduct);
                }

            }

            async Task<Order> CreateOrder()
            {
                var order = new Order
                {
                    Id = await _orderService.GenerateUniqueTrackingCodeAsync(),
                    UserId = _userService.CurrentUser.Id,
                    Status = Database.Models.Enums.OrderStatus.Created
                };
                await _dbContext.Orders.AddAsync(order);
                return order;
            }
        }

    }
}
