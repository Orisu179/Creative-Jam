using UnityEngine;

public class UndoButton : MonoBehaviour
{
    [SerializeField] private CreateDrink cupController;

    private void OnMouseDown()
    {
        cupController.Undo();
    }
}