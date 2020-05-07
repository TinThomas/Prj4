using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Login;
using Login.SigninControl.Modules;
using Login.SignupControl;
using Login.WalletControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrctioneerSamlet.Models;

namespace LoginVue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : Controller
    {
        private CoreService SignupService;

        public SignupController()
        {
            SignupService = new CoreService(new SigninControl(new UserName(), 
                    new Password()),new Signup());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] InternalMessage msg)
        {
            string complete = await SignupService.CreateUser(msg.msg);
            if (complete != null)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}