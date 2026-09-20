using UnityEngine;

[CreateAssetMenu(fileName = "IngredientObject", menuName = "Scriptable Objects/IngredientObject")]
public class IngredientObject : ScriptableObject
{
    public Ingredient IngredientType;
    public AK.Wwise.Event pickUpDragEvent;
    public AK.Wwise.Event dropDragEvent;
    public Sprite OnDragSprite;
}
