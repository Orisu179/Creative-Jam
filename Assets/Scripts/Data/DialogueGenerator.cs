using System.Collections.Generic;
using UnityEngine;

public class DialogueGenerator
{
    public static string GenerateDialogue(List<DrinkAttribute> attributes)
    {
        if (attributes.Count != 3)
        {
            Debug.LogError($"Current attribute list does not equal to 3, list:{attributes.ToString()}");
            return "";
        }

        // TODO: @Holly
        return "";
    }
    
}
