using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions; // Importing a library for MD5 hashing.
using nop.gg.Entities;
using nop.gg.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace nop.gg.Controllers
{
    [Authorize] // Specifies that only authenticated users can access the methods in this controller.
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;

        public AccountController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        [AllowAnonymous] // Specifies that the "Login" method can be accessed by both authenticated and unauthenticated users.
        public IActionResult Login()
        {
            // Returns the login view.
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Hash the provided password using MD5.
                string hashedPassword = DoMD5HashedString(model.Password);

                // Query the database for a user with matching username and hashed password.
                User user = _databaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == hashedPassword);

                if (user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), @$"{model.Username} is locked.");
                        return View(model);
                    }

                    // Create a list of claims to be stored in a cookie.
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new(ClaimTypes.Name, user.FullName ?? string.Empty));
                    claims.Add(new(ClaimTypes.Name, user.Role));
                    claims.Add(new("Username", user.Username));

                    // Create a claims identity and principal and sign in the user.
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    // Redirect to the "Index" action of the "Home" controller.
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                }
            }
            return View(model);
        }

        // Helper method to hash a string using MD5 with a salt.
        private string DoMD5HashedString(string s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            // Returns the registration view.
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already in use.");
                    return View(model);
                }

                // Hash the provided password using MD5.
                string hashedPassword = DoMD5HashedString(model.Password);

                // Create a new User instance and add it to the database.
                User user = new User
                {
                    Username = model.Username,
                    Password = hashedPassword
                };

                _databaseContext.Users.Add(user);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "User could not be added.");
                }
                else
                {
                    // Redirect to the "Login" action.
                    return RedirectToAction(nameof(Login));
                }
            }

            return View(model);
        }

        // Displays the user's profile.
        public IActionResult Profile()
        {
            // Load and display profile information.
            ProfileInfoLoader();
            return View();
        }

        // Helper method to load user profile information.
        private void ProfileInfoLoader()
        {
            // Get the user's ID from claims and retrieve user information from the database.
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);

            // Set the "FullName" in ViewData for use in the profile view.
            ViewData["FullName"] = user.FullName;
        }

        [HttpPost]
        public IActionResult ProfileChangedFullName([Required][StringLength(50)] string? fullname)
        {
            if (ModelState.IsValid)
            {
                // Get the user's ID from claims and retrieve user information from the database.
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);

                // Update the user's full name and save changes to the database.
                user.FullName = fullname;
                _databaseContext.SaveChanges();

                // Redirect back to the "Profile" action.
                return RedirectToAction(nameof(Profile));
            }
            // Reload profile information and return the "Profile" view.
            ProfileInfoLoader();
            return View("Profile");
        }

        [HttpPost]
        public IActionResult ProfileChangePassword([Required][MinLength(6)][MaxLength(16)] string? password)
        {
            if (ModelState.IsValid)
            {
                // Get the user's ID from claims and retrieve user information from the database.
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);

                // Hash the provided password using MD5.
                string hashedPassword = DoMD5HashedString(password);

                // Update the user's password and save changes to the database.
                user.Password = hashedPassword;
                _databaseContext.SaveChanges();

                // Set a result message in ViewData and reload profile information.
                ViewData["result"] = "PasswordChanged";
            }

            // Reload profile information and return the "Profile" view.
            ProfileInfoLoader();
            return View("Profile");
        }

        // Logs the user out and redirects to the "Login" action.
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
