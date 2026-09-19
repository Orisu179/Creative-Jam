using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day : MonoBehaviour
{
    public float SatisfactionLevel;
    public List<Drink> OrderList;
    // public int NumOfDeath;

    private void Start() //for testing
    {
        Debug.Log("--- STARTING RECIPE TESTS ---");

        Debug.Log("--- FIRST DRINK ---");
        Day.DrinkManager test = new Day.DrinkManager();
        Debug.Log(test.CreatedDrink);


        test.AddIngredient(Ingredient.Ice);
        test.AddIngredient(Ingredient.Ice);
        test.AddIngredient(Ingredient.Ice);
        test.AddIngredient(Ingredient.Ice);
        test.AddIngredient(Ingredient.Ice);
        test.AddIngredient(Ingredient.Ice);
        Debug.Log(test.IngredientListtoString());
        test.PrepareDrink();


        Debug.Log(test.CreatedDrink);
        Debug.Log(test.IngredientListtoString());

        Debug.Log("--- SECOND DRINK ---");
        test = new Day.DrinkManager();
        Debug.Log(test.CreatedDrink);
        //"Sprite","FruitMix","MagicalFlower","FruitSyrup"
        test.AddIngredient(Ingredient.Sprite);
        test.AddIngredient(Ingredient.FruitMix);
        test.AddIngredient(Ingredient.MagicalFlower);
        test.AddIngredient(Ingredient.FruitSyrup);
        test.PrepareDrink();
       

        Debug.Log(test.CreatedDrink);
        Debug.Log(test.IngredientListtoString());



        Debug.Log("--- THIRD DRINK ---");
        test = new Day.DrinkManager();
        Debug.Log(test.CreatedDrink);
        test.PrepareDrink();


        Debug.Log(test.CreatedDrink);
        Debug.Log(test.IngredientListtoString());

        Debug.Log("--- RECIPE TESTS COMPLETE ---");
    }

    
    public class DrinkManager
    {
        public Drink? CreatedDrink = null;
        public List<Ingredient> IngredientList = new List<Ingredient>();

        public void AddIngredient(Ingredient ingredient){
            IngredientList.Add(ingredient);
        }

        public string IngredientListtoString(){
            string commaSeparated = string.Join(", ", IngredientList);
            return commaSeparated;
        }

        public void RemoveIngredient(){ //to undo
            if (IngredientList.Count > 0)
            {
                IngredientList.RemoveAt(IngredientList.Count - 1);
            }
        }

        public void ResetMix(){
            IngredientList.Clear();
        }
        public void ResetDrink(){
            CreatedDrink = null;
        }
    
        public Drink PrepareDrink()
        {
            // Format and count the input ingredients into a sorted list
            var providedSignature = IngredientList
                .GroupBy(i => i)
                .Select(g => new { Ingredient = g.Key, Count = g.Count() })
                .OrderBy(x => x.Ingredient)
                .ToList();

            // Scan through each drink to find a perfect signature match
            foreach (Drink drink in Enum.GetValues(typeof(Drink)))
            {
                List<Ingredient> recipe = GetRecipeIngredients(drink);

                var recipeSignature = recipe
                    .GroupBy(i => i)
                    .Select(g => new { Ingredient = g.Key, Count = g.Count() })
                    .OrderBy(x => x.Ingredient)
                    .ToList();

                // Check if the provided ingredients perfectly match the recipe
                if (providedSignature.SequenceEqual(recipeSignature))
                {
                    CreatedDrink = drink;
                    ResetMix();
                    return drink;
                }
            }

            ResetMix();

            Debug.Log("no recipe matched");

            CreatedDrink = Drink.invalid;
            return Drink.invalid;
        }


        public static List<Ingredient> GetRecipeIngredients(Drink drink)
        {
            return drink switch
            {
                Drink.blueLatte => new List<Ingredient>
                {
                    Ingredient.Milk,
                    Ingredient.Ice,
                    Ingredient.Expresso,
                    Ingredient.Tea
                },

                Drink.icedAmericano => new List<Ingredient>
                {
                    Ingredient.Ice,
                    Ingredient.Expresso,
                    Ingredient.Tea,
                    Ingredient.Sugar
                },

                Drink.magicMatcha => new List<Ingredient>
                {
                    Ingredient.Expresso,
                    Ingredient.Tea,
                    Ingredient.Sugar,
                    Ingredient.Sprite
                },

                Drink.strawberryLemonade => new List<Ingredient>
                {
                    Ingredient.Tea,
                    Ingredient.Sugar,
                    Ingredient.Sprite,
                    Ingredient.FruitMix
                },

                Drink.icedLatte => new List<Ingredient>
                {
                    Ingredient.Sugar,
                    Ingredient.Sprite,
                    Ingredient.FruitMix,
                    Ingredient.MagicalFlower
                },

                Drink.dragonfruitTea => new List<Ingredient>
                {
                    Ingredient.Sprite,
                    Ingredient.FruitMix,
                    Ingredient.MagicalFlower,
                    Ingredient.FruitSyrup
                },

                Drink.lycheeMilkTea => new List<Ingredient>
                {
                    Ingredient.FruitMix,
                    Ingredient.MagicalFlower,
                    Ingredient.FruitSyrup,
                    Ingredient.Milk
                },

                Drink.magicalTea => new List<Ingredient>
                {
                    Ingredient.MagicalFlower,
                    Ingredient.FruitSyrup,
                    Ingredient.Milk,
                    Ingredient.Ice
                },

                Drink.cremeBruleeMilkTea => new List<Ingredient>
                {
                    Ingredient.FruitSyrup,
                    Ingredient.Milk,
                    Ingredient.Ice,
                    Ingredient.Milk
                },

                Drink.rainbowSoda => new List<Ingredient>
                {
                    Ingredient.Milk,
                    Ingredient.Ice,
                    Ingredient.Milk,
                    Ingredient.Ice
                },

                Drink.creamSoda => new List<Ingredient>
                {
                    Ingredient.Ice,
                    Ingredient.Milk,
                    Ingredient.Ice,
                    Ingredient.Expresso
                },

                _ => new List<Ingredient>()
            };
        }

    }
}
