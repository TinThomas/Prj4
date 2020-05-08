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
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;

namespace LoginVue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : Controller
    {
        private CoreService SignupService;
        private IPasswordRepository _pass;
        private IUsernameRepository _user;

        public SignupController(PassModelContext Passdb, UserModelContext userdb)
        {
            SignupService = new CoreService(new SigninControl(new UserName(), 
                    new Password()),new Signup());
            _pass = new PasswordRepository(Passdb);
            _user = new UsernameRepository(userdb);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]HeaderRequest request)
        {
            UsernameEntity user = new UsernameEntity()
            {
                Username = request.Username,
                Email = request.Email
            };
            bool check = await _user.CheckUser(user);
            if (check)
            {
                string id = await _user.addUser(user);
                if (!string.IsNullOrEmpty(id))
                {
                    PasswordEntity pass = new PasswordEntity()
                    {
                        UserId = id,
                        Password = request.Password
                    };

                    _pass.CreatePassword(pass);
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] HeaderRequest request)
        {
            UsernameEntity user = new UsernameEntity();
            user.UserId = User.Identity.Name;
            if (request.Username != null)
            {
                user.Username = request.Username;
                _user.updateUsername(user);
            }

            if (request.Email != null)
            {
                user.Email = request.Email;
            }

            if (request.Password != null)
            {
                PasswordEntity pass = new PasswordEntity();
                pass.UserId = User.Identity.Name;
                pass.Password = request.Password;
                _pass.updatePassword(pass);
            }

            return Ok();

        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete()
        {
            _user.DeleteUser(User.Identity.Name);
            _pass.DeletePassword(User.Identity.Name);
            return Ok();
        }
    }
}