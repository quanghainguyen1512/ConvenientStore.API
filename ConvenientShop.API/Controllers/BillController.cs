using System.Collections.Generic;
using System.Linq;
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
    [Route("api/bills")]
    public class BillController : Controller
    {
        private readonly IBillRepository _repo;
        private readonly IAccountRepository _arepo;
        private readonly IProductRepository _prepo;
        private readonly IStaffRepository _srepo;

        public BillController(IBillRepository repo, IStaffRepository srepo, IAccountRepository arepo, IProductRepository prepo)
        {
            this._srepo = srepo;
            this._arepo = arepo;
            this._prepo = prepo;
            this._repo = repo;
        }

        [HttpGet]
        public IActionResult GetBills(int accountId = -1)
        {
            if (!_arepo.AuthorizeUser(accountId, Permission.ViewBillHistory))
                return Unauthorized();
            var bills = _repo.GetBills();
            var result = Mapper.Map<IEnumerable<BillSimpleDto>>(bills);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBill(int id, bool includeDetail = true, int accountId = -1)
        {
            if (!_arepo.AuthorizeUser(accountId, Permission.ViewBillHistory))
                return Unauthorized();

            var bill = _repo.GetBill(id, includeDetail);
            if (bill is null)
                return NotFound();

            var result = Mapper.Map<BillDto>(bill);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult PostBill([FromBody] BillForOperationsDto bill, int accountId = -1)
        {
            if (bill is null) return BadRequest();
            if (!_arepo.AuthorizeUser(accountId, Permission.AddBill))
                return Unauthorized();

            var (isValid, errs) = bill.Validate();
            if (!isValid)
                return BadRequest(errs);
            if (!_srepo.StaffExists(bill.StaffId))
                return NotFound();

            for (var i = 0; i < bill.BillDetails.Count(); i++)
            {
                var bd = bill.BillDetails.ElementAt(i);
                var (isBdValid, errors) = bd.Validate(i);
                if (!isBdValid)
                    return BadRequest(errors);
                if (!_prepo.ProductDetailExists(bd.BarCode))
                    return NotFound();
            }

            var billToAdd = Mapper.Map<Bill>(bill);
            return _repo.AddBill(billToAdd) ?
                new StatusCodeResult(StatusCodes.Status201Created) :
                new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}