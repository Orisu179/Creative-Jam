using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class MixingCupArea : MonoBehaviour
    {
        // C# event for script subscribers: passes (DropArea, DroppedObject)
        public static event Action<MixingCupArea, Ingredient> OnAnyItemDropped;
        // public static event Action<Ingredient> OnItemDropped;

        // Inspector-friendly event
        // [SerializeField] private UnityEvent<GameObject> onDropResponse;

        // public void ReceiveDrop(Ingredient droppedObject)
        // {
        //     // Fire event callbacks
        //     // OnItemDropped?.Invoke(droppedObject);
        //     OnAnyItemDropped?.Invoke(this, droppedObject);
        // }
        public void ReceiveDrop(Ingredient droppedObject)
        {
            Debug.Log("Ingredient dropped: " + droppedObject);

            OnAnyItemDropped?.Invoke(this, droppedObject);
        }
    }
}