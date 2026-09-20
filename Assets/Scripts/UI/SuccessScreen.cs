using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private void Start()
    {
        GlobalFields.Instance.Score = 10.0f;
        _textMeshPro.text = GlobalFields.Instance.Score.ToString(CultureInfo.InvariantCulture);
    }

    public void ToMenuScreen()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ToStartState()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
