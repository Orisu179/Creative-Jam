using UnityEngine;
using Gameplay;

public class CreateDrink : MonoBehaviour
{
    public DrinkManager drinkManager;

    [SerializeField] private SpriteRenderer cupSprite;
    [SerializeField] private MixingCupArea mixingCupArea;

    [Header("Drink Sprites")]
    [SerializeField] private Sprite INVALIDSprite;
    [SerializeField] private Sprite POUF_SODALICIOUSSprite;
    [SerializeField] private Sprite WHIMS_TEASprite;
    [SerializeField] private Sprite HOT_LATTESprite;
    [SerializeField] private Sprite OVER_THE_STARS_SODASprite;
    [SerializeField] private Sprite UNDER_THE_ABYSS_MATCHASprite;
    [SerializeField] private Sprite LYCHEE_MILK_TEASprite;
    [SerializeField] private Sprite LATTE_BLEUSprite;
    [SerializeField] private Sprite CREME_BRULEE_MILK_TEASprite;
    [SerializeField] private Sprite ICED_SODA_AMERICANOSprite;
    [SerializeField] private Sprite STRAWBERRY_ADESprite;



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
            Drink.INVALID => INVALIDSprite,
            Drink.POUF_SODALICIOUS => POUF_SODALICIOUSSprite,
            Drink.WHIMS_TEA => WHIMS_TEASprite,
            Drink.HOT_LATTE => HOT_LATTESprite,
            Drink.OVER_THE_STARS_SODA => OVER_THE_STARS_SODASprite,
            Drink.UNDER_THE_ABYSS_MATCHA => UNDER_THE_ABYSS_MATCHASprite,
            Drink.LYCHEE_MILK_TEA => LYCHEE_MILK_TEASprite,
            Drink.LATTE_BLEU => LATTE_BLEUSprite,
            Drink.CREME_BRULEE_MILK_TEA => CREME_BRULEE_MILK_TEASprite,
            Drink.ICED_SODA_AMERICANO => ICED_SODA_AMERICANOSprite,
            Drink.STRAWBERRY_ADE => STRAWBERRY_ADESprite,
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