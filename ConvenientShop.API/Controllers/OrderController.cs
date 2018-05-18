using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using ConvenientShop.API.Services;
using ConvenientShop.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConvenientShop.API.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repo;
        private readonly IAccountRepository _arepo;
        private readonly IStaffRepository _srepo;
        private readonly IProductRepository _prepo;

        public OrderController(IOrderRepository repo, IAccountRepository arepo, IStaffRepository srepo, IProductRepository prepo)
        {
            this._repo = repo;
            this._arepo = arepo;
            this._srepo = srepo;
            this._prepo = prepo;
        }

        [HttpGet]
        public IActionResult GetAllOrders(int accountId = -1)
        {
            if (!_arepo.AuthorizeUser(accountId, Permission.ViewOrder))
                return Unauthorized();
            var ords = _repo.GetAllOrders();
            var result = Mapper.Map<IEnumerable<OrderDto>>(ords);
            return Ok(result);
        }

        [HttpGet("{id}/details")]
        public IActionResult GetDetailsForOrder(int id, int accountId = -1)
        {
            if (!_arepo.AuthorizeUser(accountId, Permission.ViewOrder))
                return Unauthorized();
            var details = _repo.GetOrderDetailsForOrder(id);
            var result = Mapper.Map<IEnumerable<OrderDetailDto>>(details);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] OrderForOperationsDto order, int accountId = -1)
        {
            if (order is null) return BadRequest();
            if (!_arepo.AuthorizeUser(accountId, Permission.OrderNewProduct))
                return Unauthorized();
            var (isValid, err) = order.Validate();
            if (!isValid)
                return BadRequest(err);

            if (!_srepo.StaffExists(order.StaffId))
                return NotFound();
            foreach (var item in order.OrderDetails)
            {
                var (isOdValid, e) = item.Validate();
                if (!isOdValid)
                    return BadRequest(e);
                if (!_prepo.ProductExists(item.ProductId))
                    return NotFound();
            }

            var orderToAdd = Mapper.Map<Order>(order);
            return _repo.AddOrder(orderToAdd) ?
                StatusCode(200, "Created Order successfully") :
                StatusCode(500, "A problem happened while handling your request.");
        }
    }
}