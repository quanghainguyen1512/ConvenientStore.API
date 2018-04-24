using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public SupplierController(ISupplierRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetSuppliers()
        {
            var sups = _repo.GetSuppliers();
            var result = Mapper.Map<IEnumerable<SupplierWithoutProductsDto>>(sups);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetSupplier(int id, bool includeProducts = false)
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
    }
}