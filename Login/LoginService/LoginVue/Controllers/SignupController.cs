using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CoreModule;
using LoginVue.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginVue.Controllers
{
    public class SignupController : Controller
    {
        TestBE be = new TestBE();
        public IActionResult Signup()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] Login val)
        {
            string newUser = JsonSerializer.Serialize(val);
            bool complete = await be.incomming(1, 3, newUser);
            if (complete)
            {
                return Ok(200);
            }
            return BadRequest(400);
        }
    }
}