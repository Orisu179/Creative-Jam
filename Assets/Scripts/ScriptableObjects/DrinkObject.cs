using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "DrinkObject", menuName = "Scriptable Objects/DrinkObject")]
public class DrinkObject : ScriptableObject
{
    public Drink DrinkType;
    public Sprite DrinkSprite;
    public List<DrinkAttribute> AttributeList;
    public List<Ingredient> IngredientList;
    public bool isPoisoned;
}
