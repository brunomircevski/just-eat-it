using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEI.Models; 

public class IngredientCategory
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [MinLength(3)]
    public string Name { get; set; }

    public List<Ingredient> Ingredients { get; set; }
}