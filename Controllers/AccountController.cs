using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

}
