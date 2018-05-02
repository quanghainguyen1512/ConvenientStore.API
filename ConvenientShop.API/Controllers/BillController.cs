using System.Collections.Generic;
using AutoMapper;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using ConvenientShop.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConvenientShop.API.Controllers
{
    [Produces("application/json")]
    [Route("api/bills")]
    public class BillController : Controller
    {
        private readonly IBillRepository _repo;
        private readonly ICustomerRepository crepo;
        private readonly IStaffRepository srepo;
        public BillController(IBillRepository repo, IStaffRepository srepo, ICustomerRepository crepo)
        {
            this.srepo = srepo;
            this.crepo = crepo;
            this._repo = repo;

        }

        [HttpGet]
        public IActionResult GetBills()
        {
            var bills = _repo.GetBills();
            var result = Mapper.Map<IEnumerable<BillDto>>(bills);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBill(int id)
        {
            var bill = _repo.GetBill(id);
            if (bill is null)
                return NotFound();

            var result = Mapper.Map<BillDto>(bill);
            return Ok(result);
        }

        // Need to test
        [HttpPost]
        public IActionResult PostBill([FromBody] BillForOperationsDto bill)
        {
            var (isValid, errs) = bill.Validate();
            if (!isValid)
                return BadRequest(errs);

            if (!crepo.CustomerExists(bill.CustomerId) || !srepo.StaffExists(bill.StaffId))
                return BadRequest();

            var billToAdd = Mapper.Map<Bill>(bill);
            return _repo.AddBill(billToAdd) ?
                StatusCode(201, "Create Successfully") :
                StatusCode(500, "A problem happened while handling your request.");
        }
        // Need to implement
        [HttpPut("{id}")]
        public IActionResult PutBill(int id, [FromBody] BillForOperationsDto billToUpdate)
        {
            return null;
        }
    }
}