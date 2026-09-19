using UnityEngine;

public class UndoButton : MonoBehaviour
{
    [SerializeField] private CupController cupController;

    private void OnMouseDown()
    {
        cupController.Undo();
    }
}