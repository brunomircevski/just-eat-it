using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEI.Models; 

public class Recipe
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [MinLength(3)]
    public string Name { get; set; }

    [MaxLength(5000)]
    public string Description { get; set; }

    [MaxLength(5000)]
    public string Manual { get; set; }

    public List<Category> Categories { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; }

    [Range(0,100000)]
    public float ?CaloriesPerServing { get; set; }

    [MaxLength(200)]
    public string ImageFileName { get; set; }

    [NotMapped]
    public IFormFile Image { get; set; }

    public bool Favorite { get; set; } = false;
    
    public int Calculate_Calories()
    {
        int Calories = 0;
        foreach (Ingredient ing in Ingredients)
        {
            if(ing.Unit == Units.g || ing.Unit==Units.ml)
            {
                Calories += ing.Calories * ing.RecipeIngredients.
            }
            
        }
    }
    */
}