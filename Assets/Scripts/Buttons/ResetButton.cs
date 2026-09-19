using UnityEngine;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private CupController cupController;

    private void OnMouseDown()
    {
        cupController.ResetCup();
    }
}