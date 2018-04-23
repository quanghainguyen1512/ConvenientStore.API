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
    [Route("api/staff")]
    public class StaffController : Controller
    {
        private IStaffRepository _repo;

        public StaffController(IStaffRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetStaffs(int userId = -1)
        {
            if (userId == -1)
                return Unauthorized();

            var staffs = _repo.GetAllStaffs();

            var result = Mapper.Map<IEnumerable<StaffDto>>(staffs);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetStaff(int id)
        {
            var staff = _repo.GetStaff(id);
            if (staff is null)
                return NotFound();

            var result = Mapper.Map<StaffDto>(staff);
            return Ok(result);
        }
    }
}