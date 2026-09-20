using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Gameplay;
using State_Machines;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Ingredient? PoisonedIngredient = null;
    public uint CurLoop { get; private set; }
    public int CustomerCounter { get; private set; }
    public List<Customer> Customers { get; private set; }
    private CustomerSpriteManager _customerSpriteManager;
    public Day CurDay { get; set; }

    [SerializeField] private Sprite placeholder;
    [SerializeField] private uint maxLoop;
    [SerializeField] private uint customerSize;
    [SerializeField] private CreateDrink createDrink;

    // States
    public StateMachine _stateMachine;
    public InitState _initState { get; set; }
    private CreateDrinkState _createDrinkState { get; set; }
    private CustomerInteractState _customerInteractState;
    public DayStartState _dayStartState { get; set; }
    private LoopFailState _loopFailedState;
    public ServeDrinkState _serveDrinkState { get; set; }



    private void Start()
    {
        DOTween.Init();

        _stateMachine = new StateMachine();
        _customerSpriteManager = GetComponentInChildren<CustomerSpriteManager>();
        // var allIngredient = (Ingredient[])Enum.GetValues(typeof(Ingredient));
        // PoisonedIngredient = allIngredient[Random.Range(0, allIngredient.Length)];
        Customers = new List<Customer>();
        CustomerCounter = 0;

        _initState = new InitState(this);
        _createDrinkState = new CreateDrinkState(this, createDrink);
        _customerInteractState = new CustomerInteractState(this, _customerSpriteManager, _createDrinkState);
        _dayStartState = new DayStartState(this, _customerInteractState);
        _loopFailedState = new LoopFailState(this, _dayStartState);
        _serveDrinkState = new ServeDrinkState(this, _customerSpriteManager, _loopFailedState, _customerInteractState);


        _stateMachine.Initialize(_initState);

        // MixingCupArea.OnAnyItemDropped += HandleDrop;
    }

    // Used in Init State
    public void GenerateCustomers()
    {
        for (var i = 0; i < customerSize; i++)
        {
            (OwlSpecies species, Accessory accessory) = CustomerSpriteManager.GetRandomOwlSpeciesAndAccessory();
            var curCustomer = new Customer("", species, accessory);
            curCustomer.Dialogue = DialogueGenerator.GenerateDialogue(curCustomer);

            Customers.Add(curCustomer);
        }
    }

    private void HandleDrop(MixingCupArea area, Ingredient ingredient)
    {
        if (ingredient == PoisonedIngredient)
        {
            Debug.Log("This is poisoned!");
            return;
        }
        Debug.Log($"The ingredient is: {ingredient.ToString()}");
    }

    public int CalculateScore(Day d)
    {
        // range from -10 to 10
        // increase or decrease by value of attribute in customer
        // if miss both major attributes, -1 extra penalty

        int score = 0;
        int penalty = 0;

        foreach (var cust_pref in d.CurrentCustomer.Preferences)
        {

            if (DrinkAttributeDatabase.GetDrinkAttributes(d.CurrentDrink ?? Drink.INVALID).Contains(cust_pref.Key)) // if attributes match
            {
                score += cust_pref.Value;
            }
            else
            {
                score -= cust_pref.Value;
                if (cust_pref.Value >= 3) // if miss a major attribute
                {
                    penalty++;
                }
            }
        }
        if (penalty == 2 && score > -10) // missed both major attributes
        {
            score--;
        }

        return score;
    }

    // InitState
    public void ResetLoop()
    {
        CurLoop = 0;
    }

    // Day Start State
    public void IncrementLoop()
    {
        CurLoop++;
    }

    public void IncrementCustomer()
    {
        CustomerCounter++;
        if (CustomerCounter < Customers.Count)
        {
            Day curDay = CurDay;
            curDay.CurrentCustomer = Customers[CustomerCounter];
            CurDay = curDay;
        }
    }

    public void ResetCustomer()
    {
        CustomerCounter = 0;
    }

    public uint GetMaxLoop()
    {
        return maxLoop;
    }

    public bool IsLastCustomer()
    {
        return CustomerCounter >= Customers.Count - 1;
    }

    public void SetCurrentDrink(Drink? drink)
    {
        Day curDay = CurDay;
        curDay.CurrentDrink = drink;
        CurDay = curDay;
    }

    public void SetSatisfactionLevel(int level)
    {
        Day curDay = CurDay;
        curDay.SatisfactionLevel = level;
        CurDay = curDay;
    }
}
