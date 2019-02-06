using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AGRental.Controllers
{
    public class loginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    [HttpPost]
    public ActionResult Index (LoginModel model)
     {
            if(ModelState.IsValid)
            {
                if(model.Username =="asdf" && model.Password =="1234")
                {
                    //FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("index", "home");
                }
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            return View();
     }

   }
}