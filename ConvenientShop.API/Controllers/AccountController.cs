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
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private IAccountRepository _repo;

        public AccountController(IAccountRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult LogIn(string encodedStr)
        {
            var data = Helpers.Helpers.Base64Decode(encodedStr);
            var extractedData = data.Split(':');
            var accountId = _repo.LogIn(extractedData[0], extractedData[1]);
            if (accountId == -1)
                return Unauthorized();
            return Ok(accountId);
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] string encodedStr, int accountId = -1)
        {
            if (!_repo.AuthorizeUser(accountId, Permission.EditAccount))
                return Unauthorized();
            var data = Helpers.Helpers.Base64Decode(encodedStr);
            var extractedData = data.Split(':');
            var acc = new AccountForOperationsDto
            {
                Username = extractedData[0],
                Password = extractedData[1],
                RoleId = int.Parse(extractedData[2])
            };

            var (isValid, err) = acc.Validate();
            if (!isValid)
                return BadRequest(err);
            var newAcc = Mapper.Map<Account>(acc);
            return _repo.CreateAccount(newAcc) ?
                StatusCode(201, "Create Successfully") :
                StatusCode(500, "A problem happened while handling your request.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id, int accountId)
        {
            if (!_repo.AuthorizeUser(accountId, Permission.DeleteAccount))
                return Unauthorized();
            if (_repo.IsAccountExists(id))
                return NotFound();

            if (!_repo.DeleteAccount(id))
                return StatusCode(500, "A problem happened while handling your request");
            return NoContent();
        }
    }
}