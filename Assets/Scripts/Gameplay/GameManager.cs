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
    private Ingredient? _poisonedIngredient = null;
    private uint _curLoop;
    public int CustomerCounter { get; private set; }
    public List<Customer> Customers { get; private set; }
    private CustomerSpriteManager _customerSpriteManager;
    public Day CurDay { get; set; }

    [SerializeField] private Sprite placeholder;
    [SerializeField] private uint maxLoop;
    [SerializeField] private uint customerSize;

    // States
    public StateMachine _stateMachine;
    public InitState _initState { get; set; }
    public CreateDrinkState _createDrinkState { get; set; }
    public CustomerInteractState _customerInteractState { get; set; }
    public DayStartState _dayStartState { get; set; }
    public LoopFailState _loopFailedState { get; set; }
    public ServeDrinkState _serveDrinkState { get; set; }
    public WinState _winState { get; set; }



    private void Start()
    {
        DOTween.Init();
        _curLoop = 0;

        _stateMachine = new StateMachine();
        _customerSpriteManager = GetComponentInChildren<CustomerSpriteManager>();
        // var allIngredient = (Ingredient[])Enum.GetValues(typeof(Ingredient));
        // _poisonedIngredient = allIngredient[Random.Range(0, allIngredient.Length)];
        Customers = new List<Customer>();
        CustomerCounter = 0;


        _initState = new InitState(this);
        _createDrinkState = new CreateDrinkState(this);
        _customerInteractState = new CustomerInteractState(this, _customerSpriteManager, _createDrinkState);
        _dayStartState = new DayStartState(this);
        _loopFailedState = new LoopFailState(this, _loopFailedState, _dayStartState);
        _serveDrinkState = new ServeDrinkState(this);
        _winState = new WinState(this);


        _stateMachine.Initialize(_initState);

        // MixingCupArea.OnAnyItemDropped += HandleDrop;

        Sequence customerSequence = DOTween.Sequence();
        _customerSpriteManager.SetSprite(Customers[0].Species, Customers[0].Accessory);
        customerSequence.Append(_customerSpriteManager.FadeIn());
        customerSequence.Append(_customerSpriteManager.FadeOut()).OnComplete(() =>
        {
            _customerSpriteManager.SetSprite(Customers[1].Species, Customers[1].Accessory);
            customerSequence.Append(_customerSpriteManager.FadeIn());
        });
    }

    public void NextLoop()
    {
        if (_curLoop >= maxLoop)
        {
            // game overscreen
            _stateMachine.ChangeState(new GameOverState(this));
        }
        // Restart
        _curLoop++;

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
        if (ingredient == _poisonedIngredient)
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
            if (DrinkAttributeDatabase.GetDrinkAttributes(d.CurrentDrink).Contains(cust_pref.Key)) // if attributes match
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
        _curLoop = 0;
    }

    // Day Start State
    public void IncrementLoop()
    {
        _curLoop++;
    }

    public void IncrementCounter()
    {
        CustomerCounter++;
    }

    public void ResetCustomer()
    {
        CustomerCounter = 0;
    }
}
