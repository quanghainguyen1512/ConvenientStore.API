using System.Collections.Generic;
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
    [Route("api/delivery")]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryRepository _repo;
        private readonly IAccountRepository _arepo;
        private readonly ISupplierRepository _srepo;
        private readonly IOrderRepository _orepo;
        private readonly IProductRepository _prepo;

        public DeliveryController(IDeliveryRepository repo, IAccountRepository arepo,
            ISupplierRepository srepo, IOrderRepository orepo, IProductRepository prepo)
        {
            this._repo = repo;
            this._arepo = arepo;
            this._srepo = srepo;
            this._orepo = orepo;
            this._prepo = prepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var des = _repo.GetAllDelivery();
            var result = Mapper.Map<IEnumerable<DeliveryDto>>(des);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetDelivery(int id, bool includeShipment = true)
        {
            var de = _repo.GetDelivery(id, includeShipment);
            if (de is null)
                return NotFound();

            return null;
        }

        [HttpPost]
        public IActionResult PostDelivery([FromBody] DeliveryForOperationsDto deli, int accountId = -1)
        {
            if (deli is null)
                return BadRequest();
            if (!_arepo.AuthorizeUser(accountId, Permission.EditInventoryDetail))
                return Unauthorized();

            var (isValid, errors) = deli.Validate();
            if (!isValid)
                return BadRequest(errors);

            foreach (var item in deli.Shipments)
            {
                (isValid, errors) = item.Validate();
                if (!isValid)
                    return BadRequest(errors);
                (isValid, errors) = item.ProductDetail.Validate();
                if (!isValid)
                    return BadRequest(errors);
                if (_prepo.ProductDetailExists(item.ProductDetail.BarCode))
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            var deliToPost = Mapper.Map<Delivery>(deli);
            return _repo.AddDelivery(deliToPost) ?
                new StatusCodeResult(StatusCodes.Status201Created) :
                new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}