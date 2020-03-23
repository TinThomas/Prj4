using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LoginVue.Controllers
{
    public class SigninController : Controller
    {
        public IActionResult Index()
        {
            return PartialView();
        }
    }
}