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

    public IActionResult List(int offset, string msg)
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
            return RedirectToAction("Error", "Home", new { msg = "Failed to fetch data from database." });
        }

        ViewBag.msg = msg;

        return View(ingredients);
    }

    public IActionResult Index()
    {
        return RedirectToAction("List");
    }

    public IActionResult Details(int id, string msg)
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
            return RedirectToAction("Error", "Home", new { msg = "Failed to fetch data from database." });
        }

        if (ingredient is null) return RedirectToAction("List");

        ViewBag.msg = msg;

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
            return RedirectToAction("Error", "Home", new { msg = "Failed to fetch data from database." });
        }

        if (ingredient is null) return RedirectToAction("List");

        return View(ingredient);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Edit(Ingredient ingredient)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.msg = "Failed to add new ingredient.";
            ViewBag.error = true;
            return Edit(ingredient.Id);
        }

        try
        {
            Ingredient _ingredient = DB.Ingredients
                .Where(i => i.Id == ingredient.Id)
                .FirstOrDefault();

            if (_ingredient is null) return RedirectToAction("Error", "Home", new { msg = "Ingredient not found." });

            _ingredient.Name = ingredient.Name;
            _ingredient.Description = ingredient.Description;
            _ingredient.Unit = ingredient.Unit;
            _ingredient.Calories = ingredient.Calories;
            _ingredient.Carbs = ingredient.Carbs;
            _ingredient.Fats = ingredient.Fats;
            _ingredient.Fiber = ingredient.Fiber;
            _ingredient.Salt = ingredient.Salt;
            _ingredient.Proteins = ingredient.Proteins;

            DB.SaveChanges();
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Failed to fetch data from database." });
        }

        return RedirectToAction("Details", "Ingredient", new { id = ingredient.Id, msg = "Ingredient succesfully updated." });
    }

    [Authorize(Roles = "admin")]
    public IActionResult Delete(int id)
    {
        try
        {
            DB.Remove(
                DB.Ingredients
                .Where(i => i.Id == id)
                .FirstOrDefault());

            DB.SaveChanges();
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Failed to remove ingredient." });
        }

        return RedirectToAction("List", "Ingredient", new { msg = "Ingredient succesfully removed." });
    }


}
