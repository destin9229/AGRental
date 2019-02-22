using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AGRental.Controllers
{
    // [Authorize]
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}