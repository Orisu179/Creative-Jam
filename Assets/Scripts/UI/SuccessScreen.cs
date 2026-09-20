using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMeshPro;
    private void Start()
    {
        if (GlobalFields.Instance.Score == null)
        {
            GlobalFields.Instance.Score = 0.0f;
        }
        _textMeshPro.text = GlobalFields.Instance.Score.ToString(CultureInfo.InvariantCulture);
    }

    public void ToMenuScreen()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ToStartState()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
