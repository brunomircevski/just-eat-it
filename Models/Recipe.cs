using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEI.Models; 

public class Recipe
{
    [Required]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Recipe_Manual { get; set; }

    public List<string> Diet { get; set; }

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

    public List<float> Weights { get; set; }

    public float Calories = 0;

    public void Calculate_Calories()
    {
        Calories = 0;
        int i = 0;
        foreach (Ingredient ing in Ingredients)
        {
            Calories += ing.Calories_per_100_g * Weights[i] / 100;
        }
    }
    public void Change_Weight(int index, int new_weight )
    {
        Weights[index] = new_weight;
    }
    public void Delete_Ingredient(int index)
    {
        Ingredients.RemoveAt(index);
        Weights.RemoveAt(index);
    }
    public void Add_Ingredient(Ingredient ing, float weight)
    {
        Ingredients.Add(ing);
        Weights.Add(weight);
    }
}