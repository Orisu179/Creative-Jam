using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class CustomerDropArea : MonoBehaviour
    {
        // Fired whenever a finished drink is successfully dropped on this area.
        public static event Action<CustomerDropArea, Drink> OnAnyCupServed;

        public void ReceiveCup(Drink drink)
        {
            Debug.Log("Cup served: " + drink);
            OnAnyCupServed?.Invoke(this, drink);
        }
    }
}