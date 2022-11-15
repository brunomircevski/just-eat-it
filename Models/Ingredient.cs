using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JEI.Models; 

public class Ingredient
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [MinLength(3)]
    public string Name { get; set; }

    public List<IngredientCategory> Categories { get; set; }
    /*
     - myslalem ze rzeczy typu warzywa,owoce itp. tylko nwm czy to potrzebne w ogole tbh xdxdxd
     - niech będzie, dobry pomysł
     */

    [Range(0,10000)]
    public float CaloriesPerUnit { get; set; }

    public Units Unit { get; set; }

    public List<Recipe> Recipes { get; set; }
}