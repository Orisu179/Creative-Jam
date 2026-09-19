using System.Collections.Generic;
using UnityEngine;

public class DialogueGenerator
{
    public static string GenerateDialogue(Customer customer)
    {
        if (customer.Preferences.Count != 3)
        {
            Debug.LogError($"Current attribute list does not equal to 3, list:{customer.Preferences.ToString()}");
            return "";
        }

        foreach (var customerPreference in customer.Preferences)
        {
            Debug.Log(customerPreference.Key);  // attribute
            Debug.Log(customerPreference.Value); // the weight
        }

        // TODO: @Holly
        return "I want this kind of mood";
    }
    
}
