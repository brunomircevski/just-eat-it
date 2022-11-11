using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEI.Models; 

public class Recipe
{
    [Required]
    public int Id { get; set; }

    //Dodać własności przepisu

    public List<Ingredient> Ingredients { get; set; }
}