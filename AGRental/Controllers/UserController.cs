using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AGRental.Data;
using AGRental.Models;
using AGRental.Helpers;
using AGRental.ViewModels;


namespace AGRental.Controllers
{
    public class UserController : Controller
    {
        private AGRentalDBContext context;

        public UserController(AGRentalDBContext dbContext)
        {
            context = dbContext;
        }

        //INDEX
        // GET: /<controller>/
        public IActionResult Index(string username)
        {
            //If the username is a Login user get username else get empty ""
            ViewBag.Username = string.IsNullOrEmpty(username as string) ? "" : username;

            //IList<User> users = context.Users.Include(c => c.Type).ToList();
            IList<User> users = context.Users.ToList();
            return View(users);
        }



        //LOGIN
        // GET: /<controller>/
        public IActionResult Login()
        { 

            // If the user is already logged in
            if (HttpContext.Session.TryGetValue("user", out byte[] value))
            {
                return RedirectToAction("Index", "User", new { username = HttpContext.Session.GetString("user") });
            }
            else
            {
                LoginViewModel userViewModel = new LoginViewModel();
                return View(userViewModel);
            }

        }

        // POST: /<controller>/

        [HttpPost]
        public IActionResult Login(LoginViewModel userFromView)
        {
            if (ModelState.IsValid)
            {
                User currentUser = context.Users.SingleOrDefault(c => c.Username == userFromView.Username);

                if ((currentUser != null) && (currentUser.Password == userFromView.Password))
                {
                    //Login Success... Greet the User
                    HttpContext.Session.SetString("user", currentUser.Username);
                    string userInSesion = HttpContext.Session.GetString("user");
                    TestFunctions.PrintConsoleMessage("LOGIN SUCCESS " + userInSesion);

                    //return Redirect("/User");
                    //return RedirectToAction("Index", "User",new { username = currentUser.Username });
                    return RedirectToAction("Index", "User", new { username = userInSesion });
                }
                else if (currentUser == null)
                {
                    // User Does not exist in the database... return custom message
                    ModelState.AddModelError("ServerError", "Sorry, we couldn't find an account with that Username");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("USER DOES NOT EXIST IN THE DATABASE");
                }
                else
                {
                    // Password Does not Match with the one in the database... return custom message
                    ModelState.AddModelError("ServerError", "Sorry, that password isn't correct.");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("PASSWORD DOES NOT MATCH BETWEEN THE FORM AND DATABASE");
                }
            }
            return View(userFromView);
        }



        //LOGOUT
        // GET: /<controller>/
        public IActionResult Logout()
        {
            //Delete or clear the Current Session
            //HttpContext.Session.Clear();

            return RedirectToAction("Index", "User", new { username = HttpContext.Session.GetString("user") });
            //return RedirectToAction("Index", "User", new { username = "" });
        }



        //SIGNUP
        // GET: /<controller>/
        public IActionResult Signup()
        {
            SignupViewModel userViewModel = new SignupViewModel();

            return View(userViewModel);
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Signup(SignupViewModel userFromView)
        {
            bool usernameAvaliable = false;

            if (ModelState.IsValid)
            {
                /*
                //User existingUser = context.Users.Find(userFromView.Username);
                //existingUser = context.Users.SingleOrDefault(c => c.Username == userFromView.Username);
                */

                try
                {
                    //Check for the availability of the selected username on the database 
                    context.Users.Single(c => c.Username == userFromView.Username);
                }
                catch
                {
                    //The username does not exist in the database
                    usernameAvaliable = true;
                }

                if (usernameAvaliable)   // If is an Avaliable Username (It needs to be unique)
                {
                    // Add the new user to my existing users table
                    User newUser = new User
                    {
                        Username = userFromView.Username,
                        FirstName = userFromView.FirstName,
                        LastName = userFromView.LastName,
                        Email = userFromView.Email,
                        Password = userFromView.Password,
                        TypeID = 2,         // Default for "Regular user", needs to be implemented for the next database update
                        MembershipID = 1    // Default for "None"
                                            //Created = DateTime.Now    //To be used when updating database, needs to be implemented for the next database update
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    // Create a new login session (Session["user"] = newUser.Username)
                    HttpContext.Session.SetString("user", newUser.Username);
                    string userInSesion = HttpContext.Session.GetString("user");
                    TestFunctions.PrintConsoleMessage("LOGIN SUCCESS " + userInSesion);

                    // Greet the new user and redirect to its dashboard (Needs to be made)
                    return RedirectToAction("Index", "User", new { username = userInSesion });
                }
                else
                {
                    // Cannot use the same username if it exist already
                    ModelState.AddModelError("ServerError", "Sorry, but this username already exists. Please try with a different one.");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("DUPLICATED USER");
                }
            }
            return View(userFromView);
        }
    }

}