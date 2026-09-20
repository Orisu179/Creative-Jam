using UnityEngine;

public class GlobalFields : MonoBehaviour
{
    public enum GameOverState
    {
        Fired,
        MaxLooped,
    }
    public static GlobalFields Instance { get; private set; }
    public float score;
    public GameOverState? State = null;
    
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
