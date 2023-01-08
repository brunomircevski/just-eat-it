using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using JEI.Models;
using JEI.DataAccess;

namespace JEI.Controllers;

public class AccountController : Controller
{
    private readonly IDB DB;
    public AccountController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUser u)
    {
        if (!ModelState.IsValid) return View();

        User user = DB.Users.Where(x => x.Email == u.Email).FirstOrDefault();

        if (user is null)
        {
            ViewBag.msg = "Email not found. Register first.";
            return View();
        }

        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: u.Password,
            salt: user.Salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        if (hash == user.PasswordHash)
        {
            string role = "user";
            if (user.IsAdmin) role = "admin";

            user.LastLoginDate = DateTime.Now;
            DB.SaveChanges();

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        ViewBag.msg = "Email or password is not correct. Try again.";
        return View();
    }

    [HttpPost]
    public IActionResult Register(LoginUser u)
    {
        if (!ModelState.IsValid) return View("Login");

        if (DB.Users.Where(x => x.Email == u.Email).FirstOrDefault() is not null)
        {
            ViewBag.msg = "This email is alredy registered.";
            return View("Login");
        }

        if (u.Password != u.Password2)
        {
            ViewBag.msg = "Passwords are not the same";
            return View("Login");
        }

        User user = new User();

        user.Email = u.Email;

        user.Salt = new byte[128 / 8];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetNonZeroBytes(user.Salt);
        }

        user.PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: u.Password,
            salt: user.Salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        DB.Users.Add(user);
        DB.SaveChanges();

        return RedirectToAction("Login", "Account");
    }

    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult ChangePassword(LoginUser user)
    {
        if (!ModelState.IsValid) return View();

        if (user.Password != user.Password2)
        {
            ViewBag.msg = "Passwords are not the same";
            ViewBag.error = true;
            return View();
        }

        User DBuser = DB.Users.Where(x => x.Id == getUserId()).FirstOrDefault();

        if (DBuser is null) return RedirectToAction("Error", "Home", new { msg = "User not found in database." });

        DBuser.PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: user.Password,
            salt: DBuser.Salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        DB.SaveChanges();

        ViewBag.msg = "Password changed successfully.";
        return View();
    }

    [Authorize]
    public IActionResult Profile()
    {
        int id = getUserId();

        if (id == 0) return RedirectToAction("Error", "Home", new { msg = "User not found." });

        User u = DB.Users.Where(x => x.Id == id).FirstOrDefault();

        if (u is null) if (id == 0) return RedirectToAction("Error", "Home", new { msg = "User not found." });

        ViewBag.user = u;

        return View();
    }

    [Authorize(Roles = "user")]
    public IActionResult PersonalData()
    {
        int id = getUserId();

        if (id == 0) if (id == 0) return RedirectToAction("Error", "Home", new { msg = "User not found." });

        User u = DB.Users.Where(x => x.Id == id).FirstOrDefault();

        if (u is null) if (id == 0) return RedirectToAction("Error", "Home", new { msg = "User not found." });

        return View(u);
    }

    [HttpPost]
    [Authorize(Roles = "user")]
    public IActionResult PersonalData(User user)
    {
        int id = getUserId();

        if (id == 0) if (id == 0) return RedirectToAction("Error", "Home", new { msg = "User not found." });

        User DBuser = DB.Users.Where(x => x.Id == id).FirstOrDefault();

        if (DBuser is null) if (id == 0) return RedirectToAction("Error", "Home", new { msg = "User not found." });

        if (ModelState.IsValid)
        {
            DBuser.BirthDate = user.BirthDate;
            DBuser.IsMan = user.IsMan;
            DBuser.Target = user.Target;
            DBuser.Activity = user.Activity;
            DBuser.Weight = user.Weight;
            DBuser.Heigth = user.Heigth;

            DB.SaveChanges();
            return RedirectToAction("Profile");
        }

        return View(DBuser);
    }

    [Authorize(Roles = "user")]
    public async Task<IActionResult> Delete(bool? confirm)
    {
        if (confirm == true)
        {

            DB.Remove(DB.Users.Where(x => x.Id == getUserId()).FirstOrDefault());
            DB.SaveChanges();

            await Logout();
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    private int getUserId()
    {
        try
        {
            int id = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            return id;
        }
        catch
        {
            return 0;
        }
    }
}
