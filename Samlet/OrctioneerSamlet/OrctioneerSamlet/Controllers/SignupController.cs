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

            return BadRequest();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] HeaderRequest request)
        {
            int wait;
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

            return Ok();

        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete()
        {
            int wait;
            wait = await _user.DeleteUser(User.Identity.Name);
            wait = await _pass.DeletePassword(User.Identity.Name);
            return Ok();
        }



        public async Task<ActionResult<string>> LoadPicture(string path)
        {
            StreamReader sr = new StreamReader(path);

            return sr.ReadLine();

        }
    }
}