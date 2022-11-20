using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEI.Models; 


public class All_Recipes
{
    [Required]
    public int Id { get; set; }

    public List<Recipe> Recipes { get; set; }

    public List<Recipe> Custom_Recipes { get; set; }
    
    public void Search_With_Diet(string diet)
    {
        Custom_Recipes.Clear();
        foreach(Recipe recipe in Recipes)
        {
            /*  POPSUŁEM bo zmieniłem model i nie wiem o co chodziło
            foreach(string str in recipe.Diet)
            {
                if(str == diet)
                {
                    Custom_Recipes.Add(recipe);
                }
            }
            */
        }
    }
    public static int Similarity(string source, string target)
    {
        if (source == target) return 0;
        if (source.Length == 0) return target.Length;
        if (target.Length == 0) return source.Length;
        int[] v0 = new int[target.Length + 1];
        int[] v1 = new int[target.Length + 1];
        for (int i = 0; i < v0.Length; i++)
            v0[i] = i;

        for (int i = 0; i < source.Length; i++)
        {
            v1[0] = i + 1;
            for (int j = 0; j < target.Length; j++)
            {
                var cost = (source[i] == target[j]) ? 0 : 1;
                v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
            }
            for (int j = 0; j < v0.Length; j++)
                v0[j] = v1[j];
        }
        return v1[target.Length];
    }

    public static double CalculateSimilarity(string source, string target)
    {
        if ((source == null) || (target == null)) return 0.0;
        if ((source.Length == 0) || (target.Length == 0)) return 0.0;
        if (source == target) return 1.0;

        int stepsToSame = Similarity(source, target);
        return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
    }

    public void Search_With_Name(string name)
    {
        Custom_Recipes.Clear();
        foreach (Recipe recipe in Recipes)
        {
            if (CalculateSimilarity(name,recipe.Name)>0.75)
            {
                Custom_Recipes.Add(recipe);
            }
        }
    }
    public void Search_With_Ingredient(string ingredient)
    {
        foreach(Recipe recipe in Recipes)
        {
            foreach(RecipeIngredient ing in recipe.RecipeIngredients)
            {
                if(ing.Ingredient.Name == ingredient)
                {
                    Custom_Recipes.Add(recipe);
                }
            }
        }
    }
}