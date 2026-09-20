using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Day
{
    public int SatisfactionLevel;
    public List<Drink> OrderList;
    public Drink? CurrentDrink;
    public Customer CurrentCustomer;
}
