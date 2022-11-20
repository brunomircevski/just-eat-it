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

    [Required]
    public Units Unit { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; }

    //Wszystko poniżej na jednostkę

    [Range(0,10000)]
    public float ?Calories { get; set; }

    [Range(0,10000)]
    public float ?Carbs { get; set; }

    [Range(0,10000)]
    public float ?Fats { get; set; }

    [Range(0,10000)]
    public float ?Fiber { get; set; }

    [Range(0,10000)]
    public float ?Salt { get; set; }

    [Range(0,10000)]
    public float ?Proteins { get; set; }

}