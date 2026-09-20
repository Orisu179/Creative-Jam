using UnityEngine;

public class PrepareButton : MonoBehaviour
{
    [SerializeField] private CreateDrink cupController;

    private void OnMouseDown()
    {
        cupController.PrepareDrink();
    }
}