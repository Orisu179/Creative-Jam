using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



// --- JSON data shape (matches drinkAttributes.json) ---

[Serializable]
public class DrinkAttributeEntry
{
    public string drink;
    public List<string> attributes;
}

[Serializable]
public class DrinkAttributeData
{
    public List<DrinkAttributeEntry> drinkAttributes;
}

// --- Loader / lookup ---

public static class DrinkAttributeDatabase
{
    private static Dictionary<Drink, List<DrinkAttribute>> _attributes;

    // Put drinkAttributes.json in a "Resources" folder, WITHOUT the .json extension in the path.
    public static void Load(string resourcePath = "drinkAttributes")
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(resourcePath);
        if (jsonFile == null)
        {
            Debug.LogError($"DrinkAttributeDatabase: could not find data at Resources/{resourcePath}.json");
            _attributes = new Dictionary<Drink, List<DrinkAttribute>>();
            return;
        }

        DrinkAttributeData data = JsonUtility.FromJson<DrinkAttributeData>(jsonFile.text);
        _attributes = new Dictionary<Drink, List<DrinkAttribute>>();

        var seenSignatures = new HashSet<string>();

        foreach (DrinkAttributeEntry entry in data.drinkAttributes)
        {
            if (!Enum.TryParse(entry.drink, out Drink drink))
            {
                Debug.LogWarning($"DrinkAttributeDatabase: unknown drink name '{entry.drink}', skipping.");
                continue;
            }

            var attributeList = new List<DrinkAttribute>();
            foreach (string attributeName in entry.attributes)
            {
                if (Enum.TryParse(attributeName, out DrinkAttribute attribute))
                {
                    attributeList.Add(attribute);
                }
                else
                {
                    Debug.LogWarning($"DrinkAttributeDatabase: unknown attribute '{attributeName}' for drink '{entry.drink}', skipping.");
                }
            }

            // Sanity check: warn (don't crash) if a drink doesn't have exactly 3 attributes,
            // or if two drinks share the exact same 3-attribute set.
            if (attributeList.Count != 3)
            {
                Debug.LogWarning($"DrinkAttributeDatabase: '{entry.drink}' has {attributeList.Count} attributes, expected 3.");
            }

            string signature = string.Join(",", attributeList.ConvertAll(a => a.ToString()).OrderBy(s => s));
            if (!seenSignatures.Add(signature))
            {
                Debug.LogWarning($"DrinkAttributeDatabase: '{entry.drink}' has the same attribute set as another drink ({signature}).");
            }

            _attributes[drink] = attributeList;
        }
    }

    public static List<DrinkAttribute> GetDrinkAttributes(Drink drink)
    {
        if (_attributes == null)
        {
            Load(); // lazy-load with default path if nobody called Load() yet
        }

        return _attributes.TryGetValue(drink, out List<DrinkAttribute> attributes)
            ? attributes
            : new List<DrinkAttribute>();
    }
}