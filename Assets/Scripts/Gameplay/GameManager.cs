using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // This should be a singleton
    // private Day curDay;
    public static GameManager Instance { get; private set; }
    private Ingredient? _poisonedIngredient = null;
    private uint _curLoop;
    private List<Customer> _customers;
    [SerializeField] private Sprite placeholder;
    [SerializeField] private uint maxLoop;
    [SerializeField] private uint customerSize;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Clear duplicates
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject); 
    }

    private void Start()
    {
        _curLoop = 0;
        var allIngredient = (Ingredient[])Enum.GetValues(typeof(Ingredient));
        _poisonedIngredient = allIngredient[Random.Range(0, allIngredient.Length)];
        _customers = new List<Customer>();
        GenerateCustomers();

        MixingCupArea.OnAnyItemDropped += HandleDrop;
    }

    public void NextLoop()
    {
        _curLoop++;
        if (_curLoop >= maxLoop)
        {
            // game overscreen
        }
        // TODO: Finish this
    }

    private void GenerateCustomers()
    {
        for (var i = 0; i < customerSize; i++)
        {
            var curCustomer = new Customer("", placeholder); 
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

    private int CalculateScore(Customer c, DrinkObject d)
    {
        // range from -10 to 10
        // increase or decrease by value of attribute in customer
        // if miss both major attributes, -1 extra penalty

        int score = 0;
        int penalty = 0;
        
        foreach (var cust_pref in c.Preferences)
        {
            if(d.AttributeList.Contains(cust_pref.Key)) // if attributes match
            {
                score += cust_pref.Value;
            }
            else
            {
                score -= cust_pref.Value;
                if(cust_pref.Value >= 3) // if miss a major attribute
                {
                    penalty++;
                }
            }
        }
        if(penalty == 2 && score > -10) // missed both major attributes
        {
            score--;
        }
        return score;
    }
}
