using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void Start()
    {
        SoundManager.Instance.PostEvent(SoundManager.SoundEvent.PlayRewindMusic);
    }

    public void goToGame()
    {
        SoundManager.Instance.PostEvent(SoundManager.SoundEvent.PlayMenuButton);
        SoundManager.Instance.PostEvent(SoundManager.SoundEvent.StopRewindMusic);
        SceneManager.LoadScene("JesseScene");
    }

    public void quitGame()
    {
        SoundManager.Instance.PostEvent(SoundManager.SoundEvent.PlayMenuButton);
        Application.Quit();
    }
}
