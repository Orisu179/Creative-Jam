using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public enum SceneStates
    {
        Menu,
        Shop,
        Barista,
        GameOver
    }
    public static GameFlowManager Instance { get; private set; }
}
