using UnityEngine;

[CreateAssetMenu(fileName = "IngredientObject", menuName = "Scriptable Objects/IngredientObject")]
public class IngredientObject : ScriptableObject
{
    public Ingredient IngredientType;
    public Sprite IngredientSprite;
}
