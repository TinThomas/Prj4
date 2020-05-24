using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;

namespace OrctioneerSamlet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserRepository _user;

        private IAddressRepository _address;
        // GET
        public UserController(UserModelContext account, WalletContext user)
        {
            _user = new UserRepository(user,account);
            _address = new AddressRepository(user);
        }

        [Authorize]
        [HttpGet("UserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            var user = await _user.getThisUser(User.Identity.Name);
            if (user == null)
            {
                user = new UserEntity();
            }
            var address = await _address.GetAddress(User.Identity.Name);
            if (address == null)
            {
                address = new AddressEntity();
            }
            var email = await _user.getEmail(User.Identity.Name);
            if (email == null)
            {
                email = "";
            }
            UserDetails myUser = new UserDetails()
            {
                Name = user.FirstName + " " + user.LastName,
                Email = email,
                Address = address.Address,
                City = address.City,
                Contry = address.Contry,
                Zipcode = address.Zipcode
            };
            return Ok(myUser);
        }

        [Authorize]
        [HttpGet("getName")]
        public async Task<IActionResult> getUsername()
        {
            var user = await _user.getThisUser(User.Identity.Name);
            if (user != null)
            {
                return Ok(user.FirstName);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> updateUser([FromBody] UserEntity user)
        {
            var myUser = await _user.getThisUser(User.Identity.Name);
            if (myUser == null)
            {
                user.userID = User.Identity.Name;
                var create = await _user.addUser(user);
                if (create > 0)
                {
                    return Ok();
                }
            }
            else
            {
                myUser.FirstName = user.FirstName;
                myUser.LastName = user.LastName;
                myUser.Address = user.Address;
                var wait = await _user.updateUser(myUser);
                if (wait > 0)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserEntity user)
        {
            user.userID = User.Identity.Name;
            var wait = await _user.addUser(user);
            if (wait > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost("UpdateAddress")]
        public async Task<IActionResult> updateAddress([FromBody] AddressEntity address)
        {
            int wait = await _address.updateAddress(address,User.Identity.Name);
            if (wait > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpDelete("User")]
        public IActionResult DeleteUser()
        {
            _address.DeleteAddress(User.Identity.Name);
            _user.DeleteUser(User.Identity.Name);
            return Ok();
        }

        [Authorize]
        [HttpDelete("Address")]
        public IActionResult DeleteAddress()
        {
            _address.DeleteAddress(User.Identity.Name);
            return Ok();
        }
        
        
    }
}