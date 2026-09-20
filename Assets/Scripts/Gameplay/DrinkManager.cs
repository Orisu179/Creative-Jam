using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrinkManager
{
    public Drink? CreatedDrink = null;
    public List<Ingredient> IngredientList = new List<Ingredient>();
    private int MaxIngredientNum = 9;

    public string IngredientListtoString(){
        Debug.Log("Current Ingredient List:");
        string commaSeparated = string.Join(", ", IngredientList);
        return commaSeparated;
    }


    public void AddIngredient(Ingredient ingredient){//to cap
        if (IngredientList.Count < MaxIngredientNum)
        {
            IngredientList.Add(ingredient);
            Debug.Log("Added "+ingredient);
            Debug.Log(this.IngredientListtoString());
        }
        else
        {
            Debug.Log("too many ingredients added already");
        }
    }

    

    public void RemoveIngredient(){ //to undo
        if (IngredientList.Count > 0)
        {
            IngredientList.RemoveAt(IngredientList.Count - 1);
            Debug.Log("Removed "+IngredientList.Last());
            Debug.Log(this.IngredientListtoString());
        }
        else
        {
            Debug.Log("No Ingredients to Remove");
        }
    }

    public void ResetMix(){
        IngredientList.Clear();
        Debug.Log("Ingredient List Cleared");
        Debug.Log(this.IngredientListtoString());

    }
    public void ResetDrink(){
        CreatedDrink = null;
        Debug.Log("Drink Emptied");
        Debug.Log(this.IngredientListtoString());
    }

    public Drink? PrepareDrink()
    {
        if(IngredientList.Count <= 0)
        {
            Debug.Log("No Ingredients to Make a Drink");
            return null;
        }
        // Format and count the input ingredients into a sorted list
        var providedSignature = IngredientList
            .GroupBy(i => i)
            .Select(g => new { Ingredient = g.Key, Count = g.Count() })
            .OrderBy(x => x.Ingredient)
            .ToList();

        // Scan through each drink to find a perfect signature match
        foreach (Drink drink in Enum.GetValues(typeof(Drink)))
        {
            List<Ingredient> recipe = RecipeDatabase.GetRecipeIngredients(drink);

            var recipeSignature = recipe
                .GroupBy(i => i)
                .Select(g => new { Ingredient = g.Key, Count = g.Count() })
                .OrderBy(x => x.Ingredient)
                .ToList();

            // Check if the provided ingredients perfectly match the recipe
            if (providedSignature.SequenceEqual(recipeSignature))
            {
                CreatedDrink = drink;
                Debug.Log("You prepared a "+drink);
                ResetMix();
                return drink;
            }
        }

        ResetMix();

        Debug.Log("no recipe matched");
        

        CreatedDrink = Drink.invalid;
        Debug.Log(this.CreatedDrink);
        return Drink.invalid;
    }
    /////////////////////////////
}