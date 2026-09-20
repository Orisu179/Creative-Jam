using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum Mood {happy, tired, sunny, chilly};

public static class DialogueBank
{  
    // top attribute -> mood mapping
    public static Dictionary<DrinkAttribute, Mood> AttributeToMood = new()
    {
        { DrinkAttribute.FRUITY, Mood.happy },
        { DrinkAttribute.SWEET, Mood.happy },
        { DrinkAttribute.STRONG, Mood.tired },
        { DrinkAttribute.MAGICAL, Mood.tired },
        { DrinkAttribute.COLD, Mood.sunny },
        { DrinkAttribute.BUBBLY, Mood.sunny },
        { DrinkAttribute.HOT, Mood.chilly },
        { DrinkAttribute.MILKY, Mood.chilly }
    };
 
    public static Dictionary<Mood, string[]> greetings = new()
    {
        { Mood.happy, new[] { "Hi! It's a great day! ", "Hey! How are you doing? ", "Hello! Love you see you again! " } },
        { Mood.tired, new[] { "[yawn] Hi... ", "Ugh, it's too early for this... ", "Oh, me? Sorry, I'm not a morning person. "} },
        { Mood.sunny, new[] { "It's another day of sun! ", "This sun... I can feel my feathers shriveling up already. ", "Thank Athena you have AC in here. "} },
        { Mood.chilly, new[] { "Man, I should have worn a jacket today. ", "The wind's rough out there! Not a good day for flying. ", "Brr, is winter coming already? " } }
    };

    // for goodbyes - search by mood, then score
    // 0: score from -10 to 0, 1: score from 1 to 5, 2: score from 6 to 10
    public static Dictionary<Mood, Dictionary<int, string[]>> goodbyes = new()
    {
        {
            Mood.happy, new()
            {
                {0, new[] {"Oh, uh- Not really what I was expecting, but.. thanks! ", "Wow, this is definitely... not what I would usually get. Thanks.. for the new experience? ", ""}},
                {1, new[] {"Thanks! ", "See you! ", "Have a good day! "}},
                {2, new[] {"Wow, you read my mind! Thanks so much! ", "You guys always deliver. Thanks so much! ", "My favourite for a reason! Thanks so much! "}}
            }
        },
        {
            Mood.tired, new()
            {
                {0, new[] {"This is mine? Well. Ok. ", "[sigh] Uh, thanks. ", "Wow, you must be tired too. It's okay. Happens to the best of us. "}},
                {1, new[] {"Thanks. ", "Great, thanks. ", "Have a good one. "}},
                {2, new[] {"Oh, thank Athena. Exactly what I need. ", "You're doing Athena's work out here. ", "Saving lives as always. Thanks. "}}
            }
        },
        {
            Mood.sunny, new()
            {
                {0, new[] {"Oh... not really the vibe I was going for, but thanks. ", "Huh, I guess this is mine? I'll drink it anyway. ", "Well, not exactly what I ordered, but enjoy the sunshine! "}},
                {1, new[] {"Thanks! Catch you later. ", "Appreciate it, enjoy the weather! ", "Thanks, stay cool out there. "}},
                {2, new[] {"Absolutely perfect for today, thank you so much! ", "This really hits the spot! Enjoy the sunshine! ", "Just what I needed for a beautiful day. Cheers! "}}
            }
        },
        {
            Mood.chilly, new()
            {
                {0, new[] {"Oh... I don't think this is what I ordered, but I just need to get going. ", "Well, it's not the comfort I was hoping for, but what do they say? The cold never bothered me anyways? ", "[shivers] Uh, I guess I'll take it. See you. "}},
                {1, new[] {"Thanks, stay warm. ", "Appreciate it, bye. ", "Thanks, I'm gonna go warm up now. "}},
                {2, new[] {"Ah, this is going to warm me right up. Thank you! ", "Perfect. The cold doesn't stand a chance now! ", "Oh, perfect! Stay warm out there! "}}
            }
        }
    };

    // dialogue bank for all attribute hints - search by mood, then drink attribute
    public static Dictionary<Mood, Dictionary<DrinkAttribute, string[]>> attr_hints = new()
    {
        { 
            Mood.happy, new() 
            {
                { DrinkAttribute.FRUITY, new[] { "I'm feeling something fresh today. ", "Throw an extra lemon on there? ", "Do you have any strawberries back there? Or something like that? "} },
                { DrinkAttribute.SWEET, new[] { "I'd love a sweet treat today. ", "Don't hold back on the syrup.", "I'm craving some sugar. You gotta live a little sometimes, you know? " } },
                { DrinkAttribute.STRONG, new[] { "Hit me with all you got! ", "I'm booked and busy today, so give me something strong! ", "I want a big kick! " } },
                { DrinkAttribute.MAGICAL, new[] { "Put a little something special in there. ", "Could I get some glitter? ", "Surprise me with something out of this world! " } },
                { DrinkAttribute.COLD, new[] { "I need something to cool me down. ", "Extra ice, please! ", "Ice cold, please! " } },
                { DrinkAttribute.BUBBLY, new[] { "Something carbonated, please. ", "Something sparkling sounds good today. ", "Give me something fizzy, please. " } },
                { DrinkAttribute.HOT, new[] { "I need something to warm me up. ", "Make it nice and toasty. ", "Gonna go curl up with a book after this. " } },
                { DrinkAttribute.MILKY, new[] { "Do you have oat milk? Soy is fine, too. No? Okay, 2%. ", "Creamy like a dream, please. ", "My body might be lactose intolerant, but my heart disagrees. " } }
            }
        },
        { 
            Mood.tired, new() 
            {
                { DrinkAttribute.FRUITY, new[] { "Something with, like, a peach, [yawn], maybe... ", "Whatever fruit you have... ", "I think I need some Vitamin C... " } },
                { DrinkAttribute.SWEET, new[] { "Give me a sugar rush... " , "Extra sugar, please... I need it..... ", "Ugh, I need the glucose... " } },
                { DrinkAttribute.STRONG, new[] { "I want my drink to take out a Victorian owlet. ", "Can I get a double shot? ...Maybe a triple? ", "As strong as you legally can.. " } },
                { DrinkAttribute.MAGICAL, new[] { "Glitter? Sure, alright. I'll take all the help I can get... ", "By any chance do you know any necromancy? ", "Some of that... [waves claws vaguely], too..... " } },
                { DrinkAttribute.COLD, new[] { "Iced is fine. ", "Extra ice... ", "Ice cold. " } },
                { DrinkAttribute.BUBBLY, new[] { "Carbonated, please... ", "Something fizzy.. ", "Maybe some bubbles would help..... " } },
                { DrinkAttribute.HOT, new[] { "Extra hot... ", "I need a hot mug in my claws.. ", "Warm would be good.. " } },
                { DrinkAttribute.MILKY, new[] { "A splash of milk. ", "Lots of... [yawn].. cream..... ", "Extra foam.. " } }
            }
        },
        { 
            Mood.sunny, new() 
            {
                { DrinkAttribute.FRUITY, new[] { "This weather's got me craving something tropical. ", "Something tart! ", "Something juicy, please. "} },
                { DrinkAttribute.SWEET, new[] { "Sweet like the sun. That doesn't really make sense, but you get me. ", "Extra sugar. " , "Extra sweet! " } },
                { DrinkAttribute.STRONG, new[] { "Robust and full-bodied. ", "I want it to hit hard! ", "I'll take a double. "} },
                { DrinkAttribute.MAGICAL, new[] { "A little golden hour shine would be perfect. ", "Squeeze in some sunshine for me. ", "Give me something exciting. " } },
                { DrinkAttribute.COLD, new[] { "Ice cold. ", "I'll definitely take this one iced. ", "Extra ice. " } },
                { DrinkAttribute.BUBBLY, new[] { "Fizzy. ", "Make that sparkling. ", "With bubbles. " } },
                { DrinkAttribute.HOT, new[] { "Hot, please. My office is still freezing. ", "Hot like the weather. ", "Still hot. You know, cold drinks are bad for the body. My grandfeather always told me that. " } },
                { DrinkAttribute.MILKY, new[] { "With some milk on top, please. ", "Lots of foam! ", "With a bit of cream. " } }
            }
        },
        { 
            Mood.chilly, new() 
            {
                { DrinkAttribute.FRUITY, new[] { "I'm not ready for winter yet... Give me something fruity. ", "Do fruits grow in the winter..? I think we have to appreciate them while they're still here. ", "A fruity flavour sounds nice. " } },
                { DrinkAttribute.SWEET, new[] { "Something indulgent. ", "Extra sweet. ", "Any maple syrup? No? I'll take vanilla, then. "} },
                { DrinkAttribute.STRONG, new[] { "Strong, please. ", "I need it running through my veins to make it to work today. ", "Give me the darkest thing you have. "} },
                { DrinkAttribute.MAGICAL, new[] { "Maybe some of that Christmas magic, if you have it already? ", "I want it to feel like watching the first snow fall. ", ""} },
                { DrinkAttribute.COLD, new[] { "Cold like my heart. ", "Iced. My feathers are freezing either way. ", "Throw a clawful of snow in there. " } },
                { DrinkAttribute.BUBBLY, new[] { "I think I'd fall asleep in the sky if I had warm milk now. ", "Fizzy, please. ", "Crisp and sparkling. "} },
                { DrinkAttribute.HOT, new[] { "Piping hot. In this weather I need it. ", "Extra hot! ", "I need this to warm my soul. " } },
                { DrinkAttribute.MILKY, new[] { "Extra creamy. ", "Lots of milk.", "With milk. I'll leave it out tonight for Stork-a." } }
            }
        }
};

}