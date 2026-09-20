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

    // Update is called once per frame
    void Update()
    {

    }

    public void returnToMain()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
