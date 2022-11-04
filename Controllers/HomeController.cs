using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JEI.Models;
using JEI.DataAccess;

namespace JEI.Controllers;

public class HomeController : Controller
{
    private readonly IDB DB;
    public HomeController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult Index()
    {
        //Tworzenie nowego użytkownika i dodawanie go do bazy
        User u = new User();

        u.Email = "email@email";
        u.PasswordHash = "12345678";
        
        DB.Users.Add(u);
        DB.SaveChanges();

        return View();
    }

    public IActionResult Privacy()
    {
        //Pobieranie listy wszystkich użtkowników
        List<User> users = DB.Users.ToList();

        //Usuwanie jednego
        DB.Remove(DB.Users.FirstOrDefault());
        DB.SaveChanges();

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
