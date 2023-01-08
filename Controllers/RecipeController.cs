using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using JEI.Models;
using JEI.DataAccess;

namespace JEI.Controllers;

public class RecipeController : Controller
{
    private readonly IDB DB;
    public RecipeController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult List(int offset, string msg)
    {
        if (offset < 0) offset = 0;

        List<Recipe> recipes = null;
        try
        {
            recipes = DB.Recipes
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

        return View(recipes);
    }

    public IActionResult Index()
    {
        return RedirectToAction("List");
    }

    public IActionResult Details(int id, string msg)
    {
        if (id == 0) return RedirectToAction("List");

        Recipe recipes = null;
        try
        {
            recipes = DB.Recipes
                .Where(i => i.Id == id)
                .FirstOrDefault();
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Failed to fetch data from database." });
        }

        if (recipes is null) return RedirectToAction("List");

        ViewBag.msg = msg;

        return View(recipes);
    }

    [Authorize(Roles = "admin")]
    public IActionResult Add()
    {
        return View();
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Add(Recipe recipe)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.msg = "Failed to add new ingredient.";
            ViewBag.error = true;
            return View();
        }

        DB.Recipes.Add(recipe);
        DB.SaveChanges();

        ViewBag.msg = "Success. Recipe added to database.";
        ViewBag.error = false;
        return View();
    }

    [Authorize(Roles = "admin")]
    public IActionResult Edit(int id)
    {
        if (id == 0) return RedirectToAction("List");

        Recipe recipe = null;
        try
        {
            recipe = DB.Recipes
                .Where(i => i.Id == id)
                .FirstOrDefault();
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Failed to fetch data from database." });
        }

        if (recipe is null) return RedirectToAction("List");

        return View(recipe);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Edit(Recipe recipe)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.msg = "Failed to add new ingredient.";
            ViewBag.error = true;
            return Edit(recipe.Id);
        }

        try
        {
            Recipe _recipe = DB.Recipes
                .Where(i => i.Id == recipe.Id)
                .FirstOrDefault();

            if (_recipe is null) return RedirectToAction("Error", "Home", new { msg = "Recipe not found." });

            _recipe.Name = recipe.Name;
            _recipe.Description = recipe.Description;
            _recipe.Manual = recipe.Manual;
            _recipe.CaloriesPerServing = _recipe.CaloriesPerServing;

            DB.SaveChanges();
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Failed to fetch data from database." });
        }

        return RedirectToAction("Details", "Ingredient", new { id = recipe.Id, msg = "Recipe succesfully updated." });
    }

    [Authorize(Roles = "admin")]
    public IActionResult Delete(int id)
    {
        try
        {
            DB.Remove(
                DB.Recipes
                .Where(i => i.Id == id)
                .FirstOrDefault());

            DB.SaveChanges();
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Failed to remove recipe." });
        }

        return RedirectToAction("List", "Ingredient", new { msg = "Recipe succesfully removed." });
    }


}
