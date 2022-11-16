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

    [MaxLength(5000)]
    public string Description { get; set; }

    [Range(0,10000)]
    public float CaloriesPerUnit { get; set; }

    [Required]
    public Units Unit { get; set; }

    public List<Recipe> Recipes { get; set; }

    public float Carbs { get; set; }

    public float Fats { get; set; }

    public float Sugars { get; set; }

    public float Proteins { get; set; }

}