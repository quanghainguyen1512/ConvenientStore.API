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
    [Route("api/staffs")]
    public class StaffController : Controller
    {
        private IStaffRepository _staffRepo;
        private readonly IBillRepository _brepo;

        public StaffController(IStaffRepository srepo, IBillRepository brepo)
        {
            this._staffRepo = srepo;
            this._brepo = brepo;
        }

        [HttpGet]
        public IActionResult GetStaffs(int userId = -1)
        {
            if (userId == -1)
                return Unauthorized();

            var staffs = _staffRepo.GetAllStaffs();

            var result = Mapper.Map<IEnumerable<StaffSimpleDto>>(staffs);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetStaff(int id)
        {
            var staff = _staffRepo.GetStaff(id);
            if (staff is null)
                return NotFound();

            var result = Mapper.Map<StaffDto>(staff);
            return Ok(result);
        }

        [HttpGet("{id}/bills")]
        public IActionResult GetBillsByStaff(int id)
        {
            var bills = _brepo.GetBillsByStaff(id);
            var result = Mapper.Map<BillForStaffDto>(bills);
            return Ok(result);
        }
    }
}