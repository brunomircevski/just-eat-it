using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JEI.Models; 

public class Ingredient
{
    [Required]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Category { get; set; }
    /*
     myslalem ze rzeczy typu warzywa,owoce itp. tylko nwm czy to potrzebne w ogole tbh xdxdxd
     */

    public float Calories_per_100_g { get; set; }

    public List<Recipe> Recipes { get; set; }
}