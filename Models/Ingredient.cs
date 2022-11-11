using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEI.Models; 

public class Ingredient
{
    [Required]
    public int Id { get; set; }

    //Dodać własności składniku

    public List<Recipe> Recipes { get; set; }
}