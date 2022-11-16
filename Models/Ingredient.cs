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

    [Range(0,10000)]
    public float CaloriesPerUnit { get; set; }

    public Units Unit { get; set; }

    public List<Recipe> Recipes { get; set; }
}