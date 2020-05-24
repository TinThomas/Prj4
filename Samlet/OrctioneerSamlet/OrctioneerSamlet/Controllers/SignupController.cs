using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private IPasswordRepository _pass;
        private IUsernameRepository _user;

        public SignupController(PassModelContext Passdb, UserModelContext userdb)
        {
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

                    int wait = await _pass.CreatePassword(pass);
                    return Ok();
                }
            }

            return BadRequest("User");
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] HeaderRequest request)
        {
            int wait = 0;
            UsernameEntity user = new UsernameEntity();
            user.UserId = User.Identity.Name;
            if (request.Username != null)
            {
                user.Username = request.Username;
                wait = await _user.updateUsername(user);
            }

            if (request.Email != null)
            {
                user.Email = request.Email;
                wait = await _user.updateEmail(user);
            }

            if (request.Password != null)
            {
                PasswordEntity pass = new PasswordEntity();
                pass.UserId = User.Identity.Name;
                pass.Password = request.Password;
                wait = await _pass.updatePassword(pass);
            }

            if (wait > 0)
            {
                return Ok();
            }

            return BadRequest();

        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            string id = User.Identity.Name;
            var deletePassword = await _pass.DeletePassword(id);
            var deleteUser = await _user.DeleteUser(id);
            if (deletePassword > 0 && deleteUser > 0)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}