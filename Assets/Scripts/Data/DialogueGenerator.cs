using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using UnityEngine;


public class DialogueGenerator
{
    private static int count = 0;
    private static int num_hints = 3;
    public static string GenerateDialogue(Customer customer)
    {
        // total customers: 10
        // in first half of day (1-5), customers will give 3 hints
        // in second half of day (6-10), customers will give 2 hints
        
        count++;
        if(count == 5)
        {
            num_hints--;
        }

        Debug.Log("Generating dialogues");
        Debug.Log($"Customer number {count}");

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

        // get the customer's attributes by descending value
        List<DrinkAttribute> da_descending = customer.Preferences
                .OrderByDescending(v => v.Value)
                .Select(v => v.Key)
                .ToList();
        Mood cmood = DialogueBank.AttributeToMood[da_descending[0]];
        Debug.Log(cmood);

        // dialogue start; [greetings] [attribute hint] x num_hints 
        string result = "";
        result += DialogueBank.greetings[cmood][UnityEngine.Random.Range(0, DialogueBank.greetings[cmood].Length)];
        for(int i = 0; i < num_hints; i++)
        {
            result += DialogueBank.attr_hints[cmood] // access mood, attribute, and random index in string[]
                    [da_descending[i]]
                    [UnityEngine.Random.Range(0, DialogueBank.attr_hints[cmood][da_descending[i]].Length)];
        };

        // move goodbye to after give drink to customer
        // result += DialogueBank.goodbyes[cmood][UnityEngine.Random.Range(0, DialogueBank.goodbyes[cmood].Length)];

        Debug.Log($"The resulting string is: {result}");

        return result;
    }
    
}
