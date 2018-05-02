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
    [Route("api/suppliers")]
    public class SupplierController : Controller
    {
        private ISupplierRepository _repo;
        private readonly IProductRepository _prepo;
        public SupplierController(ISupplierRepository repo, IProductRepository prepo)
        {
            this._prepo = prepo;
            this._repo = repo;
        }

        [HttpGet]
        public IActionResult GetSuppliers()
        {
            var sups = _repo.GetSuppliers();
            var result = Mapper.Map<IEnumerable<SupplierWithoutProductsDto>>(sups);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetSupplier")]
        public IActionResult GetSupplier(int id, bool includeProducts = true)
        {
            var sup = _repo.GetSupplier(id, includeProducts);
            if (sup is null)
                return NotFound();

            if (includeProducts)
            {
                var result = Mapper.Map<SupplierDto>(sup);
                return Ok(result);
            }

            var resultWithoutProduct = Mapper.Map<SupplierWithoutProductsDto>(sup);
            return Ok(resultWithoutProduct);
        }

        [HttpPost]
        public IActionResult PostSupplier([FromBody] SupplierWithoutProductsDto supplier)
        {
            if (supplier is null)
                return BadRequest();

            var (isValid, errors) = supplier.Validate();

            if (!isValid)
                return BadRequest(errors);

            var supToAdd = Mapper.Map<Supplier>(supplier);
            return _repo.AddSupplier(supToAdd) ?
                StatusCode(201, "Create Successfully") :
                StatusCode(500, "A problem happened while handling your request.");
        }

        [HttpDelete("{id}/products/{proId}")]
        public IActionResult DeleteProductFromSupplier(int id, int proId)
        {
            if (!_repo.SupplierExists(id))
                return NotFound();

            var proToDel = _prepo.GetProduct(proId, false);
            if (proToDel is null)
                return NotFound();

            if (!_repo.DeleteProductFromSupplier(id, proToDel))
                return StatusCode(500, "A problem happened while handling your request.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            var sup = _repo.GetSupplier(id, false);
            if (sup is null)
                return NotFound();

            if (!_repo.DeleteSupplier(sup))
                return StatusCode(500, "A problem happened while handling your request.");
            return NoContent();
        }
    }
}