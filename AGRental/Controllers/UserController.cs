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
using Microsoft.AspNetCore.Session;


namespace AGRental.Controllers
{
    public class UserController : Controller
    {
        private AGRentalDBContext context;

        public UserController(AGRentalDBContext dbContext)
        {
            context = dbContext;
        }

        //INDEX     GET: /<controller>
        public IActionResult Index(string username)
        {
            //If the username is a Login user get username else get empty ""
            ViewBag.Username = string.IsNullOrEmpty(username as string) ? "" : username;

            //Creates an empty list
            IList<User> users = new List<User>();

            //Verfies if an "admin" is logged in
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                users = context.Users.ToList();
                return View(users);
            }

            //Verfies if an "user" is logged in
            else if(HttpContext.Session.GetString("Type") == "user")
            {
                users = context.Users.Where(c => c.User_ID == HttpContext.Session.GetInt32("UserID")).ToList();
                return View(users);
            }

            //Returns error message that a user is not logged in
            else
            {
                ViewBag.ErrorMessage = "You must be logged into to gain access to this feature";
                return View(users);
            }
        }


        //LOGIN    GET: /<controller>/
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

        // POST: /<controller>
        [HttpPost]
        public IActionResult Login(LoginViewModel userFromView)
        {
            if (ModelState.IsValid)
            {
                //Checks if the current User is in the username table
                User currentUser = context.Users.SingleOrDefault(c => c.Username == userFromView.Username);

                //Checks if the current User and passworkd matches
                if ((currentUser != null) && (currentUser.Password == userFromView.Password))
                {
                    // Login Successful. Greets the User
                    //
                    HttpContext.Session.SetString("user", currentUser.Username);
                    HttpContext.Session.SetInt32("UserID", currentUser.User_ID);
                    string userInSesion = HttpContext.Session.GetString("user");
                    string userType = context.UserType.SingleOrDefault(c => c.UserTypeID == currentUser.UserTypeID).UserTypeName;
                    HttpContext.Session.SetString("Type", userType);

                    //Redirects user to User Index
                    return RedirectToAction("Index", "User", new { username = userInSesion });
                }

                else if (currentUser == null)
                {
                    //If User does not exist in the User database. Return Error message
                    ModelState.AddModelError("ServerError", "Sorry, we couldn't find an account with that Username");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("USER DOES NOT EXIST IN THE DATABASE");
                }

                else
                {
                    //If password does not match with the one in the database for that user. Returns Error message
                    ModelState.AddModelError("ServerError", "Sorry, that password isn't correct.");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("PASSWORD DOES NOT MATCH BETWEEN THE FORM AND DATABASE");
                }
            }
            return View(userFromView);
        }



        //LOGOUT       GET: /<controller>
        public IActionResult Logout()
        {
            //Clears the Current Session
            HttpContext.Session.Clear();
            TempData["logoutMessage"] = "You have successfully logged out";
            
            // Logs User out and redirects to its AGRental Index
            return RedirectToAction("Index", "AGRental", new { username = HttpContext.Session.GetString("user") });
        }

        //SIGNUP       GET: /<controller>
        public IActionResult Signup()
        {
            SignupViewModel userViewModel = new SignupViewModel();
            return View(userViewModel);
        }

        // POST: /<controller>
        [HttpPost]
        public IActionResult Signup(SignupViewModel userFromView)
        {
            //Sets the current User to false
            bool usernameAvaliable = false;

            if (ModelState.IsValid)
            {
                try
                {
                    //Checks if the selected username is in the User database 
                    context.Users.Single(c => c.Username == userFromView.Username);
                }
                catch
                {
                    //The username does not exist in the User database
                    usernameAvaliable = true;
                }

                //If the User is not in the database then the user can be added to the User database
                if (usernameAvaliable)
                {
                    // Adds new user to Users table
                    // Sets the users to user not a admin
                    User newUser = new User
                    {
                        Username = userFromView.Username,
                        FirstName = userFromView.FirstName,
                        LastName = userFromView.LastName,
                        Email = userFromView.Email,
                        Password = userFromView.Password,
                        UserTypeID = 2
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    // Create a new login session (Session["user"] = newUser.Username)
                    //
                    HttpContext.Session.SetString("User", newUser.Username);
                    HttpContext.Session.SetInt32("UserID", newUser.User_ID);
                    string userInSesion = HttpContext.Session.GetString("User");
                    string userType = context.UserType.SingleOrDefault(c => c.UserTypeID == newUser.UserTypeID).UserTypeName;
                    HttpContext.Session.SetString("Type", userType);
                    TestFunctions.PrintConsoleMessage("LOGIN SUCCESS " + userInSesion);

                    // Greets the new user and redirect to its User Index
                    return RedirectToAction("Index", "User", new { username = userInSesion });
                }
                else
                {
                    //Error message that user already exists
                    ModelState.AddModelError("ServerError", "Sorry, but this username already exists. Please try with a different one.");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("DUPLICATED USER");
                }
            }
            return View(userFromView);
        }
    }

}