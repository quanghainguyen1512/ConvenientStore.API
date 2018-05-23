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
    [Route("api/staffs")]
    public class StaffController : Controller
    {
        private readonly IStaffRepository _sRepo;
        private readonly IBillRepository _brepo;
        private readonly IAccountRepository _arepo;

        public StaffController(IStaffRepository srepo, IBillRepository brepo, IAccountRepository arepo)
        {
            this._sRepo = srepo;
            this._brepo = brepo;
            this._arepo = arepo;
        }

        [HttpGet]
        public IActionResult GetStaffs(int accountId = -1)
        {
            if (!_arepo.AuthorizeUser(accountId, Permission.ViewAllStaffInfo))
                return Unauthorized();

            var staffs = _sRepo.GetAllStaffs();

            var result = Mapper.Map<IEnumerable<StaffSimpleDto>>(staffs);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetStaff(int id, int accountId = -1)
        {
            if (!_arepo.AuthorizeUser(accountId, Permission.ViewOneStaffInfo))
                return Unauthorized();
            var staff = _sRepo.GetStaff(id);
            if (staff is null)
                return NotFound();

            var result = Mapper.Map<StaffDto>(staff);
            return Ok(result);
        }

        [HttpGet("{id}/bills")]
        public IActionResult GetBillsByStaff(int id)
        {
            if (!_sRepo.StaffExists(id))
                return NotFound();
            var bills = _brepo.GetBillsByStaff(id);
            var result = Mapper.Map<IEnumerable<BillSimpleDto>>(bills);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult PostStaff([FromBody] StaffForOperationsDto staff, int accountId = -1)
        {
            if (staff is null)
                return BadRequest();
            if (!_arepo.AuthorizeUser(accountId, Permission.EditStaffInfo))
                return Unauthorized();
            var (isValid, err) = staff.Validate();
            if (!isValid)
                return BadRequest(err);
            var staffToAdd = Mapper.Map<Staff>(staff);
            return _sRepo.AddStaff(staffToAdd) ?
                new StatusCodeResult(StatusCodes.Status201Created) :
                new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpPut("{id}")]
        public IActionResult PutStaff(int id, [FromBody] StaffForOperationsDto staff, int accountId = -1)
        {
            if (staff is null)
                return BadRequest();
            if (!_arepo.AuthorizeUser(accountId, Permission.EditStaffInfo))
                return Unauthorized();
            var (isValid, err) = staff.Validate();
            if (!isValid)
                return BadRequest(err);
            if (!_sRepo.StaffExists(id))
                return NotFound();
            var staffToUpdate = Mapper.Map<Staff>(staff);
            staffToUpdate.StaffId = id;
            return _sRepo.UpdateStaff(staffToUpdate) ?
                new StatusCodeResult(StatusCodes.Status204NoContent) :
                new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

    }
}