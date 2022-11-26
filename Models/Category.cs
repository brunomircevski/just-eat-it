using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEI.Models; 

public class Category
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [MinLength(3)]
    public string Name { get; set; }

    public List<Recipe> Recipes { get; set; }

    [NotMapped]
    public int RecipesCount { get; set; }

    [Required]
    public Boolean Preferable { get; set; }

    public List<User> PreferingUsers { get; set; }
}