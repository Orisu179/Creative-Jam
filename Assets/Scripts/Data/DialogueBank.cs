using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum Mood {happy, tired, sunny, chilly};

public static class DialogueBank
{  
    // top attribute -> mood mapping
    public static Dictionary<DrinkAttribute, Mood> AttributeToMood = new()
    {
        { DrinkAttribute.fruity, Mood.happy },
        { DrinkAttribute.sweet, Mood.happy },
        { DrinkAttribute.strong, Mood.tired },
        { DrinkAttribute.magical, Mood.tired },
        { DrinkAttribute.cold, Mood.sunny },
        { DrinkAttribute.bubbly, Mood.sunny },
        { DrinkAttribute.hot, Mood.chilly },
        { DrinkAttribute.milky, Mood.chilly }
    };
 
    public static Dictionary<Mood, string[]> greetings = new()
    {
        { Mood.happy, new[] { "happy greeting 1" } },
        { Mood.tired, new[] { "tired greeting 1" } },
        { Mood.sunny, new[] { "sunny greeting 1" } },
        { Mood.chilly, new[] { "chilly greeting 1" } }
    };

    public static Dictionary<Mood, string[]> goodbyes = new()
    {
        { Mood.happy, new[] { "happy goodbye 1" } },
        { Mood.tired, new[] { "tired goodbye 1" } },
        { Mood.sunny, new[] { "sunny goodbye 1" } },
        { Mood.chilly, new[] { "chilly goodbye 1" } }
    };

    // dialogue bank for all attribute hints - search by mood, and then drink attribute
    public static Dictionary<Mood, Dictionary<DrinkAttribute, string[]>> attr_hints = new()
    {
        { 
            Mood.happy, new() 
            {
                { DrinkAttribute.fruity, new[] { "happy fruity hint 1" } },
                { DrinkAttribute.sweet, new[] { "happy sweet hint 1" } },
                { DrinkAttribute.strong, new[] { "happy strong hint 1" } },
                { DrinkAttribute.magical, new[] { "happy magical hint 1" } },
                { DrinkAttribute.cold, new[] { "happy cold hint 1" } },
                { DrinkAttribute.bubbly, new[] { "happy bubbly hint 1" } },
                { DrinkAttribute.hot, new[] { "happy hot hint 1" } },
                { DrinkAttribute.milky, new[] { "happy milky hint 1" } }
            }
        },
        { 
            Mood.tired, new() 
            {
                { DrinkAttribute.fruity, new[] { "tired fruity hint 1" } },
                { DrinkAttribute.sweet, new[] { "tired sweet hint 1" } },
                { DrinkAttribute.strong, new[] { "tired strong hint 1" } },
                { DrinkAttribute.magical, new[] { "tired magical hint 1" } },
                { DrinkAttribute.cold, new[] { "tired cold hint 1" } },
                { DrinkAttribute.bubbly, new[] { "tired bubbly hint 1" } },
                { DrinkAttribute.hot, new[] { "tired hot hint 1" } },
                { DrinkAttribute.milky, new[] { "tired milky hint 1" } }
            }
        },
        { 
            Mood.sunny, new() 
            {
                { DrinkAttribute.fruity, new[] { "sunny fruity hint 1" } },
                { DrinkAttribute.sweet, new[] { "sunny sweet hint 1" } },
                { DrinkAttribute.strong, new[] { "sunny strong hint 1" } },
                { DrinkAttribute.magical, new[] { "sunny magical hint 1" } },
                { DrinkAttribute.cold, new[] { "sunny cold hint 1" } },
                { DrinkAttribute.bubbly, new[] { "sunny bubbly hint 1" } },
                { DrinkAttribute.hot, new[] { "sunny hot hint 1" } },
                { DrinkAttribute.milky, new[] { "sunny milky hint 1" } }
            }
        },
        { 
            Mood.chilly, new() 
            {
                { DrinkAttribute.fruity, new[] { "chilly fruity hint 1" } },
                { DrinkAttribute.sweet, new[] { "chilly sweet hint 1" } },
                { DrinkAttribute.strong, new[] { "chilly strong hint 1" } },
                { DrinkAttribute.magical, new[] { "chilly magical hint 1" } },
                { DrinkAttribute.cold, new[] { "chilly cold hint 1" } },
                { DrinkAttribute.bubbly, new[] { "chilly bubbly hint 1" } },
                { DrinkAttribute.hot, new[] { "chilly hot hint 1" } },
                { DrinkAttribute.milky, new[] { "chilly milky hint 1" } }
            }
        }
};

}