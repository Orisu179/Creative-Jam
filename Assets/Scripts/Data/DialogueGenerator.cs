using System.Collections.Generic;
using UnityEngine;

public class DialogueGenerator
{
    public static string GenerateDialogue(Customer customer)
    {
        Debug.Log("Generating dialogues");
        if (customer.Preferences.Count != 3)
        {
            Debug.LogError($"Current attribute list does not equal to 3, list:{customer.Preferences.ToString()}");
            return "";
        }

        foreach (var customerPreference in customer.Preferences)
        {
            Debug.Log($"The current attribute is: {customerPreference.Key} and the current weight is: {customerPreference.Value}");  // attribute
        }
        // TODO: @Holly
        // Generate the prompt according to the attribute and weight
        // weight will also be 1-2 for one random attribute, 3-5 for the other two
        // They will add up to 10
        // If you need to check/modify the attributes, go to DrinkAttribute.cs

        var result = "replace here";
        Debug.Log($"The resulting string is: {result}");

        return result;
    }
    
}
