using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginVue.Models;
using CoreModule;
using System.Text.Json;
using System.Text.Json.Serialization;
using LoginVue.utilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace LoginVue.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SigninController : Controller
    {
        private readonly TestBE _a;
        public Login _myModel;

        public TestBE.test myState;

        public SigninController()
        {
            TestBE context = new TestBE();
            Login myModel = new Login();
            _a = context;
            _myModel = myModel;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostSignin([FromBody] internalMessage val)
        {
            bool check;
            if (val.id == 1)
            {
                check = await _a.incomming(1, val.id, val.msg);
                if (check)
                {
                    return Ok(200);
                }
            }

            if (val.id == 2)
            {
                check = await _a.incomming(1, val.id, val.msg);
                return Ok(200);
            }
            else
            {
                return BadRequest(400);
            }
        }

    }
}