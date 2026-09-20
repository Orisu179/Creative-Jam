using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// --- JSON data shape (matches recipes.json) ---

[Serializable]
public class RecipeEntry
{
    public string drink;
    public List<string> ingredients;
}

[Serializable]
public class RecipeData
{
    public List<RecipeEntry> recipes;
}

// --- Loader / lookup ---

public static class RecipeDatabase
{
    private static Dictionary<Drink, List<Ingredient>> _recipes;

    // Call once (e.g. from a bootstrap script) before GetRecipeIngredients is used.
    // Put recipes.json in a "Resources" folder, WITHOUT the .json extension in the path.
    public static void Load(string resourcePath = "recipes")
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(resourcePath);
        if (jsonFile == null)
        {
            Debug.LogError($"RecipeDatabase: could not find recipes at Resources/{resourcePath}.json");
            _recipes = new Dictionary<Drink, List<Ingredient>>();
            return;
        }

        RecipeData data = JsonUtility.FromJson<RecipeData>(jsonFile.text);
        _recipes = new Dictionary<Drink, List<Ingredient>>();

        foreach (RecipeEntry entry in data.recipes)
        {
            if (!Enum.TryParse(entry.drink, out Drink drink))
            {
                Debug.LogWarning($"RecipeDatabase: unknown drink name '{entry.drink}' in recipes.json, skipping.");
                continue;
            }

            var ingredientList = new List<Ingredient>();
            foreach (string ingredientName in entry.ingredients)
            {
                if (Enum.TryParse(ingredientName, out Ingredient ingredient))
                {
                    ingredientList.Add(ingredient);
                }
                else
                {
                    Debug.LogWarning($"RecipeDatabase: unknown ingredient '{ingredientName}' for drink '{entry.drink}', skipping.");
                }
            }

            _recipes[drink] = ingredientList;
        }
    }

    public static List<Ingredient> GetRecipeIngredients(Drink drink)
    {
        if (_recipes == null)
        {
            Load(); // lazy-load with default path if nobody called Load() yet
        }

        return _recipes.TryGetValue(drink, out List<Ingredient> ingredients)
            ? ingredients
            : new List<Ingredient>();
    }
}