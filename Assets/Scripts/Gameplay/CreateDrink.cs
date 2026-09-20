using UnityEngine;
using Gameplay;

public class CreateDrink : MonoBehaviour
{
    private DrinkManager drinkManager;

    [SerializeField] private SpriteRenderer cupSprite;
    [SerializeField] private MixingCupArea mixingCupArea;

    [Header("Drink Sprites")]
    [SerializeField] private Sprite invalidSprite;
    [SerializeField] private Sprite blueLatteSprite;
    [SerializeField] private Sprite icedAmericanoSprite;
    [SerializeField] private Sprite magicMatchaSprite;
    [SerializeField] private Sprite strawberryLemonadeSprite;
    [SerializeField] private Sprite hotLatteSprite;
    [SerializeField] private Sprite dragonfruitTeaSprite;
    [SerializeField] private Sprite lycheeMilkTeaSprite;
    [SerializeField] private Sprite magicalTeaSprite;
    [SerializeField] private Sprite cremeBruleeMilkTeaSprite;
    [SerializeField] private Sprite rainbowSodaSprite;
    [SerializeField] private Sprite creamSodaSprite;

    private void Awake()
    {
        drinkManager = new DrinkManager();
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
        Drink? result = drinkManager.PrepareDrink();
        ChangeDrinkSprite(result);
    }
    public void RemoveDrink()
    {
        ChangeDrinkSprite(null);
    }

    private void ChangeDrinkSprite(Drink? drink)
    {
        cupSprite.sprite = drink switch
        {
            Drink.invalid=>invalidSprite,
            Drink.blueLatte=>blueLatteSprite,
            Drink.icedAmericano=>icedAmericanoSprite,
            Drink.magicMatcha=>magicMatchaSprite,
            Drink.strawberryLemonade=>strawberryLemonadeSprite,
            Drink.hotLatte=>hotLatteSprite,
            Drink.dragonfruitTea=>dragonfruitTeaSprite,
            Drink.lycheeMilkTea=>lycheeMilkTeaSprite,
            Drink.magicalTea=>magicalTeaSprite,
            Drink.cremeBruleeMilkTea=>cremeBruleeMilkTeaSprite,
            Drink.rainbowSoda=>rainbowSodaSprite,
            Drink.creamSoda=>creamSodaSprite,

            _ => null
        };
    }

    public void ResetCup()
    {
        RemoveDrink();
        drinkManager.ResetMix();
        drinkManager.ResetDrink();
    }
}