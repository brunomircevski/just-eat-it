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

    public List<RecipeCategory> Categories { get; set; }

    //Wegańskie, wegetariańskie, ale też np obiad, kolacja itp, kategorie są jednocześnie tagami do wyszukiwania

    /*byle co pierwsze z googla
    Dieta śródziemnomorska
    Dieta fleksitariańska
    Dieta DASH
    Dieta wolumetryczna
    Dieta wegańska
    Dieta peskatariańska
    Dieta ketogeniczna
    Dieta wegetarianska
    takie znalazlem*/

    public List<Ingredient> Ingredients { get; set; }

    [Range(0,100000)]
    public float CaloriesPerServing { get; set; }

    /* TO TRZEBA INACZEJ ZROBIĆ, nie  da się tak do bazy zapisać
    public List<float> Weights { get; set; }

    public void Calculate_Calories()
    {
        Calories = 0;
        int i = 0;
        foreach (Ingredient ing in Ingredients)
        {
            Calories += ing.Calories_per_100_g * Weights[i] / 100;
        }
    }
    */
}