using UnityEngine;

public class PrepareButton : MonoBehaviour
{
    [SerializeField] private CupController cupController;

    private void OnMouseDown()
    {
        cupController.PrepareDrink();
    }
}