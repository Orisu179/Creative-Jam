using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // dont w about global state
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject fired;
    [SerializeField] private GameObject maxLooped;
    void Start()
    {
        SoundManager.Instance.PostEvent(SoundManager.SoundEvent.StopLoopZeroMusic);
        SoundManager.Instance.PostEvent(SoundManager.SoundEvent.StopRewindMusic);
        fired.SetActive(false);
        maxLooped.SetActive(false);

        if (GlobalFields.Instance != null)
        {
            if (GlobalFields.Instance.State == GlobalFields.GameOverState.Fired)
            {
                maxLooped.SetActive(false);
                fired.SetActive(true);
            }
            else
            {
                fired.SetActive(false);
                maxLooped.SetActive(true);
            }
        }
        else
        {
            Debug.Log("it's null");
        }
    }

    public void returnToMain()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
