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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;


namespace OrctioneerSamlet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _configuration;
        private CoreService LoginService;
        private readonly IPasswordRepository _pass;
        private readonly IUsernameRepository _user;

        public LoginController(IConfiguration configuration, PassModelContext Passdb, UserModelContext userdb)
        {
            _configuration = configuration;
            LoginService = new CoreService(new SigninControl(
                new UserName(), new Password()),new Signup());
            _pass = new PasswordRepository(Passdb);
            _user = new UsernameRepository(userdb);

        }

        [Authorize]
        [HttpPost("Token")]
        public async Task<IActionResult> PostToken()
        {
            return Ok(User.Identity.Name);
        }

        [HttpPost("UserN")]
        public async Task<IActionResult> PostUsername([FromBody]UsernameEntity request)
        {
            Console.WriteLine(request.Username);
            string id;
            if (request.Username != null)
            {
                id = await _user.validateUsername(request.Username);
            }
            else
            {
                id = await _user.validateEmail(request.Email);
            }
            //int id = await LoginService.validateUsername(request.msg);
            if (!string.IsNullOrEmpty(id))
            { 
                return Ok(id);
            }

            return BadRequest();
        }

        [HttpPost("Pass")]
        public async Task<IActionResult> PostPassword([FromBody] PasswordEntity data)
        {
            bool valid = await _pass.validatePassword(data);
           // bool complete = await LoginService.validatePassword(val.id,val.msg);
            if (valid)
            {
                TokenControl myToken = new TokenControl(_configuration);
                string token = await myToken.GenerateToken(data.UserId);
                Console.WriteLine(token);
                return Ok(token);
            }

            return BadRequest();
        }
    }
}
