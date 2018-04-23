using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConvenientShop.API.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private IAccountRepository _repo;

        public AccountController(IAccountRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult LogIn(string username = null, string password = null)
        {
            if (username is null || password is null)
                return BadRequest();
            var (isSuccessful, account) = _repo.LogIn(username, password);
            if (!isSuccessful)
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
            return Ok(account);
        }
    }
}