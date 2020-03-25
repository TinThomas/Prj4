using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginVue.Models;
using CoreModule;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace LoginVue.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SigninController : Controller
    {
        private readonly TestBE _a;
        private readonly Login _myModel;

        public TestBE.test myState;

        public SigninController()
        {
            TestBE context = new TestBE();
            Login myModel = new Login();
            _a = context;
            _myModel = myModel;
        }

        public IActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> PostSignin([FromBody] internalMessage val)
        {
            bool check;
            check = await _a.incomming(1,val.id,val.msg);
            if (check)
            {
                return Ok(200);
            }
            else
            {
                return BadRequest(400);
            }
        }

    }
}