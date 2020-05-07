using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Login;
using Login.SigninControl.Modules;
using Login.SignupControl;
using Login.WalletControl;
using Login.WalletControl.Modules;
using OrctioneerSamlet.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrctioneerSamlet.Models;


namespace OrctioneerSamlet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _configuration;
        private CoreService LoginService;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            LoginService = new CoreService(new SigninControl(
                new UserName(), new Password()),new Signup());
        }

        [Authorize]
        [HttpPost("Token")]
        public async Task<IActionResult> PostToken([FromBody]InternalMessage request)
        {
            return Ok(User.Identity.Name);
        }

        [HttpPost("UserN")]
        public async Task<IActionResult> PostUsername([FromBody]InternalMessage request)
        {
            int id = await LoginService.validateUsername(request.msg);
            if (id != -1)
            { 
                return Ok(id);
            }

            return BadRequest();
        }

        [HttpPost("Pass")]
        public async Task<IActionResult> PostPassword([FromBody] InternalMessage val)
        {
            bool complete = await LoginService.validatePassword(val.id,val.msg);
            if (complete)
            {
                TokenControl myToken = new TokenControl(_configuration);
                string token = await myToken.GenerateToken(val.id);
                Console.WriteLine(token);
                return Ok(token);
            }

            return BadRequest();
        }
    }
}
