using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class FailLoopMenu : MonoBehaviour
{
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetRemainingLoop(uint remainingLoops)
    {
        // put in the num of remaining days
        // Loops left: {remaining_days}
    }

    public void SetDrinks(List<Drink> drinks)
    {
        // display the list
        // map names to assets (?)
    }
}