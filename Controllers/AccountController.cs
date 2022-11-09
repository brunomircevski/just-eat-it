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

        if(user is null) {
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
            if(user.isAdmin) role = "admin";

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

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(LoginUser u)
    {
        if (!ModelState.IsValid) return View();

        if (DB.Users.Where(x => x.Email == u.Email).FirstOrDefault() is not null) 
        {
            ViewBag.msg = "This email is alredy registered.";
            return View();
        }

        if (u.Password != u.Password2) 
        {
            ViewBag.msg = "Passwords are not the same";
            return View();
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
    public IActionResult Profile()
    {
        int id = getUserId();

        if(id == 0) return RedirectToAction("Index", "Home");

        User u = DB.Users.Where(x => x.Id == id).FirstOrDefault();

        if(u is null) return RedirectToAction("Index", "Home");

        ViewBag.user = u;

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
