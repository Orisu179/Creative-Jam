using System.Collections.Generic;
using UnityEngine;

public struct Customers
{
    public Dictionary<DrinkAttribute, int> Preferences { get; private set; }
    public string Dialogue { get; private set; }
    public Sprite Sprite { get; private set; }

    public Customers(Dictionary<DrinkAttribute, int> keyValuePair, string dialogue, Sprite sprite)
    {
        Preferences = keyValuePair;
        Dialogue = dialogue;
        Sprite = sprite;
    }
}
