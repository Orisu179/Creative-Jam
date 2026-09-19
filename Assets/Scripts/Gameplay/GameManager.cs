using System;
using System.Collections.Generic;
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
}
