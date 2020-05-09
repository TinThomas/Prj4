using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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
        private readonly IPasswordRepository _pass;
        private readonly IUsernameRepository _user;

        public LoginController(IConfiguration configuration, PassModelContext Passdb, UserModelContext userdb)
        {
            _configuration = configuration;
            _pass = new PasswordRepository(Passdb);
            _user = new UsernameRepository(userdb);

        }

        [HttpPost("UserN")]
        public async Task<IActionResult> PostUsername([FromBody]UsernameEntity request)
        {
            string id;
            if (request.Username != null)
            {
                id = await _user.validateUsername(request.Username);
            }
            else
            {
                id = await _user.validateEmail(request.Email);
            }
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
            if (valid)
            {
                TokenControl myToken = new TokenControl(_configuration);
                string token =  myToken.GenerateToken(data.UserId);
                return Ok(token);
            }

            return BadRequest();
        }
    }
}
