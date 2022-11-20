using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JEI.Models;
using JEI.DataAccess;

namespace JEI.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error(string msg)
    {
        ViewBag.msg = msg;
        return View();
    }
}
