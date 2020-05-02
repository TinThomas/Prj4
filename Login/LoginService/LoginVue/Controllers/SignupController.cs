using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CoreModule;
using LoginVue.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupController;
using SignupController.Models;

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
            UserDataContext userDatadb = new UserDataContext();
            UserNContext userNamedb = new UserNContext();
            PassContext passworddb = new PassContext();
            
            //At this stage we have no userData besides the ID
            //Creating this object automatically generates an ID
            UserDataDB userData = new UserDataDB();

            //Create new username and password database entries with the same key
            UserNDB userName = new UserNDB(userData.UserId);
            userName.Email = val.email;
            userName.UserName = val.username;
            PassDB password = new PassDB(userData.UserId);
            password.Password = val.password;

            userDatadb.Add(userData);
            userNamedb.Add(userName);
            passworddb.Add(password);

            //update the databases
            try
            {
                userDatadb.SaveChanges();
                userNamedb.SaveChanges();
                passworddb.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(200);

            //string newUser = JsonSerializer.Serialize(val);
            //bool complete = await be.incomming(1, 3, newUser);
            //bool complete = true;
            //if (complete)
            //{
            //    return Ok(200);
            //}
            //return BadRequest(400);
        }
    }
}