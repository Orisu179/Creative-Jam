using UnityEngine;
using Gameplay;

public class CupController : MonoBehaviour
{
    private Day.DrinkManager drinkManager;

    [SerializeField] private SpriteRenderer cupSprite;
    [SerializeField] private MixingCupArea mixingCupArea;

    [Header("Drink Sprites")]
    [SerializeField] private Sprite invalidSprite;
    [SerializeField] private Sprite blueLatteSprite;
    [SerializeField] private Sprite icedAmericanoSprite;
    [SerializeField] private Sprite magicMatchaSprite;
    [SerializeField] private Sprite strawberryLemonadeSprite;
    [SerializeField] private Sprite icedLatteSprite;
    [SerializeField] private Sprite dragonfruitTeaSprite;
    [SerializeField] private Sprite lycheeMilkTeaSprite;
    [SerializeField] private Sprite magicalTeaSprite;
    [SerializeField] private Sprite cremeBruleeMilkTeaSprite;
    [SerializeField] private Sprite rainbowSodaSprite;
    [SerializeField] private Sprite creamSodaSprite;

    private void Awake()
    {
        drinkManager = new Day.DrinkManager();
        mixingCupArea.ReceiveDrop(Ingredient.Milk);
    }

    private void OnEnable()
    {
        MixingCupArea.OnAnyItemDropped += HandleIngredientDropped;
    }

    private void OnDisable()
    {
        MixingCupArea.OnAnyItemDropped -= HandleIngredientDropped;
    }

    private void HandleIngredientDropped(
        MixingCupArea cupArea,
        Ingredient ingredient)
    {
        // Is this the cup that actually received the ingredient?
        if (cupArea != mixingCupArea)
            return;

        Debug.Log("Adding " + ingredient + " to this cup.");

        drinkManager.AddIngredient(ingredient);
    }

    public void Undo()
    {
        drinkManager.RemoveIngredient();
    }

    public void PrepareDrink()
    {
        Drink result = drinkManager.PrepareDrink();

        if (result == Drink.invalid)
        {
            Debug.Log("Invalid drink!");
            return;
        }

        ChangeDrinkSprite(result);
    }

    private void ChangeDrinkSprite(Drink drink)
    {
        cupSprite.sprite = drink switch
        {
            Drink.blueLatte=>blueLatteSprite,
            Drink.icedAmericano=>icedAmericanoSprite,
            Drink.magicMatcha=>magicMatchaSprite,
            Drink.strawberryLemonade=>strawberryLemonadeSprite,
            Drink.icedLatte=>icedLatteSprite,
            Drink.dragonfruitTea=>dragonfruitTeaSprite,
            Drink.lycheeMilkTea=>lycheeMilkTeaSprite,
            Drink.magicalTea=>magicalTeaSprite,
            Drink.cremeBruleeMilkTea=>cremeBruleeMilkTeaSprite,
            Drink.rainbowSoda=>rainbowSodaSprite,
            Drink.creamSoda=>creamSodaSprite,

            _ => invalidSprite
        };
    }

    public void ResetCup()
    {
        drinkManager.ResetMix();
        drinkManager.ResetDrink();
    }
}