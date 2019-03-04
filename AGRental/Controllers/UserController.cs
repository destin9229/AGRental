using AGRental.Data;
using AGRental.Helpers;
using AGRental.Models;
using AGRental.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


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
        public IActionResult Index(string username)
        {
            //If the username is a Login user get username else get empty ""
            ViewBag.Username = string.IsNullOrEmpty(username as string) ? "" : username;

            //Creates an empty list called users
            IList<User> users = new List<User>();

            //Verfies if an "admin" is logged in
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                //Sets the users to User in the eetable
                users = context.Users.ToList();

                //Return views of users
                return View(users);
            }

            //Verfies if an "user" is logged in
            else if(HttpContext.Session.GetString("Type") == "user")
            {
                //Checks the Users table to see if the current user
                users = context.Users.Where(c => c.User_ID == HttpContext.Session.GetInt32("UserID")).ToList();
                return View(users);
            }

            else
            {
                //Returns error message that a user is not logged in
                ViewBag.ErrorMessage = "You must be logged into to gain access to this feature";
                return View(users);
            }
        }

        //LOGIN
        public IActionResult Login()
        { 
            // If the user is already logged in
            if (HttpContext.Session.TryGetValue("user", out byte[] value))
            {
                //Redirects User it the User Index
                return RedirectToAction("Index", "User", new { username = HttpContext.Session.GetString("user") });
            }
            else
            {
                //Creates a userViewModel object for the LoginViewModel
                LoginViewModel userViewModel = new LoginViewModel();
                return View(userViewModel);
            }
        }

        // POST
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
                    //Sets the current user to Username
                    HttpContext.Session.SetString("user", currentUser.Username);

                    //Sets tbe current UserID to currentUser.User_ID
                    HttpContext.Session.SetInt32("UserID", currentUser.User_ID);

                    //Gets the user and sets it to userInSession
                    string userInSession = HttpContext.Session.GetString("user");

                    //Looks for the current UserTypeID session and sets it to userType
                    string userType = context.UserType.SingleOrDefault(c => c.UserTypeID == currentUser.UserTypeID).UserTypeName;

                    //Sets the current Type to userType
                    HttpContext.Session.SetString("Type", userType);

                    //Redirects user to User Index
                    return RedirectToAction("Index", "User", new { username = userInSession });
                }

                else if (currentUser == null)
                {
                    //Error message if User does not exist in the User database
                    ModelState.AddModelError("ServerError", "Sorry, we couldn't find an account with that Username");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("USER DOES NOT EXIST IN THE DATABASE");
                }

                else
                {
                    //Error message if password does not match with the one in the database for that user
                    ModelState.AddModelError("ServerError", "Sorry, that password isn't correct.");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("PASSWORD DOES NOT MATCH BETWEEN THE FORM AND DATABASE");
                }
            }

            //Returns the userFromView View
            return View(userFromView);
        }

        //LOGOUT
        public IActionResult Logout()
        {
            //Clears the Current Session
            HttpContext.Session.Clear();
            TempData["logoutMessage"] = "You have successfully logged out";
            
            // Logs User out and redirects to its AGRental Index
            return RedirectToAction("Index", "AGRental", new { username = HttpContext.Session.GetString("user") });
        }

        //SIGNUP
        public IActionResult Signup()
        {
            //Creates a userViewModel object for the SignUpViewmodel
            SignupViewModel userViewModel = new SignupViewModel();
            return View(userViewModel);
        }

        //POST
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
                    //Creates a newUser object for the User table
                    User newUser = new User
                    {
                        //Sets the userFromView.Username to Username
                        Username = userFromView.Username,

                        //Sets the userFromView.Username to Username
                        FirstName = userFromView.FirstName,

                        //Sets the userFromView.LastName to LastName
                        LastName = userFromView.LastName,

                        //Sets the userFromView.Email to Email
                        Email = userFromView.Email,

                        //Sets the userFromView.Password to Password
                        Password = userFromView.Password,

                        //Sets the UserTypeID to 2 so they are a user
                        UserTypeID = 2
                    };

                    //Adds the newUser to the User table
                    context.Users.Add(newUser);

                    //Saves changes to table
                    context.SaveChanges();

                    //Sets the User session to the newUser.Username
                    HttpContext.Session.SetString("User", newUser.Username);

                    //Sets the User session to the newUser.User_ID
                    HttpContext.Session.SetInt32("UserID", newUser.User_ID);

                    //Gets the User and sets it to userInSession
                    string userInSession = HttpContext.Session.GetString("User");

                    //Gets the userTypeID and sets it to userType
                    string userType = context.UserType.SingleOrDefault(c => c.UserTypeID == newUser.UserTypeID).UserTypeName;

                    //Sets the userType for the session
                    HttpContext.Session.SetString("Type", userType);

                    //Prints that the user has successfully logged in
                    TestFunctions.PrintConsoleMessage("LOGIN SUCCESS " + userInSession);

                    // Greets new user and redirect them to User Index
                    return RedirectToAction("Index", "User", new { username = userInSession });
                }
                else
                {
                    //Error message that user already exists in the database
                    ModelState.AddModelError("ServerError", "Sorry, but this username already exists. Please try with a different one.");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("DUPLICATED USER");
                }
            }
            //Returns userFromView View
            return View(userFromView);
        }
    }

}