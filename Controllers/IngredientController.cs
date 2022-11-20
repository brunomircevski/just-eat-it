using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using JEI.Models;
using JEI.DataAccess;

namespace JEI.Controllers;

public class IngredientController : Controller
{
    private readonly IDB DB;
    public IngredientController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult List(int offset)
    {
        if (offset < 0) offset = 0;

        List<Ingredient> ingredients = null;
        try
        {
            ingredients = DB.Ingredients
                .OrderBy(i => i.Name)
                .Skip(offset)
                .Take(50)
                .ToList();
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Sorry. Failed to fetch data from database." });
        }

        return View(ingredients);
    }

    public IActionResult Index()
    {
        return RedirectToAction("List");
    }

    public IActionResult Details(int id)
    {
        if (id == 0) return RedirectToAction("List");

        Ingredient ingredient = null;
        try
        {
            ingredient = DB.Ingredients
                .Where(i => i.Id == id)
                .FirstOrDefault();
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Sorry. Failed to fetch data from database." });
        }

        if(ingredient is null) return RedirectToAction("List");

        return View(ingredient);
    }

    [Authorize(Roles = "admin")]
    public IActionResult Add()
    {
        return View();
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Add(Ingredient ingredient)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.msg = "Failed to add new ingredient.";
            ViewBag.error = true;
            return View();
        }

        DB.Ingredients.Add(ingredient);
        DB.SaveChanges();

        ViewBag.msg = "Success. Ingredient added to database.";
        ViewBag.error = false;
        return View();
    }

    [Authorize(Roles = "admin")]
    public IActionResult Edit(int id)
    {
        return View();
    }

    [Authorize(Roles = "admin")]
    public IActionResult Delete(int id)
    {
        return View();
    }


}
