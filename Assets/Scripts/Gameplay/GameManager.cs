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
    // This should be a singleton
    // private Day curDay;
    public static GameManager Instance { get; private set; }
    private Ingredient? _poisonedIngredient = null;
    private uint _curLoop;
    private int _customerCounter;
    private List<Customer> _customers;
    private StateMachine _stateMachine;
    private CustomerSpriteManager _customerSpriteManager;
    [SerializeField] private Sprite placeholder;
    [SerializeField] private uint maxLoop;
    [SerializeField] private uint customerSize;

    private void Start()
    {
        DOTween.Init();
        _curLoop = 0;

        _stateMachine = new StateMachine();
        _customerSpriteManager = GetComponentInChildren<CustomerSpriteManager>();
        var allIngredient = (Ingredient[])Enum.GetValues(typeof(Ingredient));
        _poisonedIngredient = allIngredient[Random.Range(0, allIngredient.Length)];
        _customers = new List<Customer>();
        GenerateCustomers();

        MixingCupArea.OnAnyItemDropped += HandleDrop;

        Sequence customerSequence = DOTween.Sequence();
        _customerSpriteManager.SetSprite(_customers[0].Species, _customers[0].Accessory);
        customerSequence.Append(_customerSpriteManager.FadeIn());
        customerSequence.Append(_customerSpriteManager.FadeOut()).OnComplete(() =>
        {
            _customerSpriteManager.SetSprite(_customers[1].Species, _customers[1].Accessory);
            customerSequence.Append(_customerSpriteManager.FadeIn());
        });
    }

    public void NextLoop()
    {
        if (_curLoop >= maxLoop)
        {
            // game overscreen
            _stateMachine.ChangeState(new LoseState());
        }
        // Restart
        _curLoop++;

    }

    private void GenerateCustomers()
    {
        for (var i = 0; i < customerSize; i++)
        {
            (OwlSpecies species, Accessory accessory) = CustomerSpriteManager.GetRandomOwlSpeciesAndAccessory();
            var curCustomer = new Customer("", species, accessory);
            curCustomer.Dialogue = DialogueGenerator.GenerateDialogue(curCustomer);
            _customers.Add(curCustomer);
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

    public int CalculateScore(Customer c, Drink d)
    {
        // range from -10 to 10
        // increase or decrease by value of attribute in customer
        // if miss both major attributes, -1 extra penalty

        int score = 0;
        int penalty = 0;

        foreach (var cust_pref in c.Preferences)
        {
            if(DrinkAttributeDatabase.GetDrinkAttributes(d).Contains(cust_pref.Key)) // if attributes match
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
}
