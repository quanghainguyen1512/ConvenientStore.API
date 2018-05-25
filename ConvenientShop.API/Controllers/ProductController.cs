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
        private readonly IProductRepository _repo;
        private readonly IAccountRepository _arepo;

        public ProductController(IProductRepository repo, IAccountRepository arepo)
        {
            this._repo = repo;
            this._arepo = arepo;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _repo.GetProducts();
            var result = Mapper.Map<IEnumerable<ProductWithoutDetailDto>>(products);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id, bool includeDetail = false)
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

        [HttpPost("{id}/details")]
        public IActionResult PostDetailToProduct(int id)
        {
            if (!_repo.ProductExists(id))
                return NotFound();

            return null;
        }

        [HttpGet("{id}/details")]
        public IActionResult GetDetailsForProduct(int id)
        {
            return null;
        }

        [HttpGet("{id}/details/{detailId}")]
        public IActionResult GetDetail(int id, int detailId)
        {
            return null;
        }

        // [HttpPost("/exports")]
    }
}