using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConvenientShop.API.Models;
using ConvenientShop.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConvenientShop.API.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _repo.GetProducts();
            var result = Mapper.Map<IEnumerable<ProductWithoutDetailDto>>(products);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id, bool includeDetail = true)
        {
            var product = _repo.GetProduct(id, includeDetail);
            if (product is null)
                return NotFound();

            if (includeDetail)
            {
                var proWithDetail = Mapper.Map<ProductDto>(product);
                return Ok(proWithDetail);
            }
            var proWithouDetail = Mapper.Map<ProductWithoutDetailDto>(product);
            return Ok(proWithouDetail);
        }
    }
}