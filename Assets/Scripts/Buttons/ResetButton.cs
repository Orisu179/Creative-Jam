using UnityEngine;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private CreateDrink cupController;

    private void OnMouseDown()
    {
        cupController.ResetCup();
    }
}