using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using ConvenientShop.API.Helpers;
using ConvenientShop.API.Models;
using ConvenientShop.API.Services;
using ConvenientShop.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConvenientShop.API.Controllers
{
    [Produces("application/json")]
    [Route("api/productdetails")]
    public class ProductDetailController : Controller
    {
        private readonly IProductRepository _prepo;
        private readonly IAccountRepository _arepo;

        public ProductDetailController(IProductRepository prepo, IAccountRepository arepo)
        {
            this._prepo = prepo;
            this._arepo = arepo;
        }

        [HttpGet]
        public IActionResult GetProductDetails(ProductDetailsResourceParameters parameters, int accountId = -1)
        {
            if (!_arepo.AuthorizeUser(accountId, Permission.ViewProductDetail))
                return Unauthorized();
            var (pds, count) = _prepo.GetAllProductDetails(parameters);
            var result = Mapper.Map<PagedList<ProductDetailSimpleDto>>(pds);
            result.AddMoreDetail(count, parameters.PageNumber, parameters.PageSize);
            return Ok(
                new
                {
                    result,
                    result.HasPrevious,
                    result.HasNext,
                    result.TotalCount,
                    result.TotalPages,
                    result.CurrentPage
                }
            );
        }

        [HttpPut("{barcode}/exports")]
        public IActionResult ExportFromRepo(string barcode, int quantity)
        {
            if (!_prepo.ProductDetailExists(barcode))
                return NotFound();
            if (quantity <= 0)
                return BadRequest();
            return _prepo.ExportFromRepo(barcode, quantity) ?
                new StatusCodeResult(StatusCodes.Status204NoContent) :
                new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}