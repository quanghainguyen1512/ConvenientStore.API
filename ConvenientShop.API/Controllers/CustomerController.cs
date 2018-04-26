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
    [Route("api/customerTypes")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repo;

        public CustomerController(ICustomerRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public IActionResult GetCusTypes()
        {
            var types = _repo.GetCustomerTypes();
            var result = Mapper.Map<IEnumerable<CustomerTypeDto>>(types);
            return Ok(result);
        }

        [HttpGet("{id}/customers/{cusId}")]
        public IActionResult GetCustomerInType(int id, int cusId)
        {
            if (!_repo.TypeExists(id))
                return NotFound();

            var cus = _repo.GetCustomer(id, cusId);
            if (cus is null)
                return NotFound();
            var result = Mapper.Map<CustomerDto>(cus);
            return Ok(result);
        }
    }
}