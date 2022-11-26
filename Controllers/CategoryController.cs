using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using JEI.Models;
using JEI.DataAccess;

namespace JEI.Controllers;

public class CategoryController : Controller
{
    private readonly IDB DB;
    public CategoryController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult List(string msg)
    {
        List<Category> categories = null;
        List<Category> userPreffered = null;
        try
        {
            if(!User.IsInRole("admin")) {
                userPreffered = DB.Users
                    .Where(u => u.Id == getUserId())
                    .Select(u => userPreffered)
                    .FirstOrDefault();               
            }

            if(userPreffered is null) userPreffered = new List<Category>();
                
            categories = DB.Categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                Preferable = c.Preferable && !userPreffered.Contains(c),
                RecipesCount = c.Recipes.Count()
            })
            .OrderByDescending(c => c.RecipesCount)
            .ToList();
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Failed to fetch data from database." });
        }

        ViewBag.msg = msg;


        return View(categories);
    }

    public IActionResult Index()
    {
        return RedirectToAction("List");
    }

    public IActionResult Recipes(int id)
    {
        if(id == 0) return RedirectToAction("List");

        try
        {
            Category category = DB.Categories
                .Include(c => c.Recipes)
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if(category is not null) return View(category);
            return RedirectToAction("Error", "Home", new { msg = "Category not found." });
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Failed to fetch data from database." });
        }
    }

    [Authorize(Roles = "admin")]
    public IActionResult Add()
    {
        return View();
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Add(Category category)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.msg = "Failed to add new category.";
            ViewBag.error = true;
            return View();
        }

        DB.Categories.Add(category);
        DB.SaveChanges();

        ViewBag.msg = "Success. Category added to database.";
        ViewBag.error = false;
        return View();
    }

    [Authorize(Roles = "admin")]
    public IActionResult Delete(int id)
    {
        try
        {
            DB.Remove(
                DB.Categories
                .Where(i => i.Id == id)
                .FirstOrDefault());

            DB.SaveChanges();
        }
        catch
        {
            return RedirectToAction("Error", "Home", new { msg = "Failed to remove category." });
        }

        return RedirectToAction("List", "Category", new { msg = "Category succesfully removed." });
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
