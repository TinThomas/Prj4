using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LoginVue.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Signup()
        {
            return PartialView();
        }
    }
}