using UnityEngine;

public class GlobalFields : MonoBehaviour
{
    public enum GameOverState
    {
        Fired,
        MaxLooped,
    }
    public static GlobalFields Instance { get; private set; }
    public float Score { get; set; }
    public GameOverState? State = null;
    public bool MouseInteractable { get; set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
           Destroy(gameObject);
           return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
