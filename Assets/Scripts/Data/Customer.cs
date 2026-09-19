using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public struct Customer
{
    public Dictionary<DrinkAttribute, int> Preferences { get; private set; }
    public string Dialogue { get; set; }
    public Sprite Sprite { get; private set; }

    public Customer(string dialogue, Sprite sprite)
    {
        Preferences = new Dictionary<DrinkAttribute, int>();
        Dialogue = dialogue;
        Sprite = sprite;
        GenerateRandomStats();
    }

    private void GenerateRandomStats(int targetSum = 10)
    {
        var allValues = (DrinkAttribute[])Enum.GetValues(typeof(DrinkAttribute));
        if (allValues.Length < 3)
        {
            throw new InvalidOperationException("StatType requires at least 3 values.");
        }

        // 1. Pick 3 distinct keys using a partial Fisher-Yates shuffle
        for (var i = 0; i < 3; i++)
        {
            var swapIndex = Random.Range(i, allValues.Length);
            (allValues[i], allValues[swapIndex]) = (allValues[swapIndex], allValues[i]);
        }

        // 2. Roll the minor stat between 1 and 3 (inclusive)
        var lowValue = UnityEngine.Random.Range(1, 3); // Returns 1, or 2
        var remaining = targetSum - lowValue;          // 7, 8, or 9

        // 3. Partition remaining points so both primary stats are >= 3
        var minPrimary = 3;
        var maxPrimary = remaining - minPrimary; // Ensures the partner stat is also >= minPrimary
        
        var highValue1 = Random.Range(minPrimary, maxPrimary + 1);
        var highValue2 = remaining - highValue1;

        // 4. Shuffle the 3 values so the minor stat isn't always bound to the same key position
        int[] values = { lowValue, highValue1, highValue2 };
        for (var i = 0; i < 3; i++)
        {
            var swapIndex = Random.Range(i, values.Length);
            (values[i], values[swapIndex]) = (values[swapIndex], values[i]);
        }

        // 5. Populate and return
        Preferences[allValues[0]] = values[0];
        Preferences[allValues[1]] = values[1];
        Preferences[allValues[2]] = values[2];
    }
}
