using System;
using System.Collections;
using UIs;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public IGameState CurrentState { get; private set; } 
    
    public void ChangeState(IGameState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    
    public void LoadSceneIfNeeded(string targetSceneName, Action onReady)
    {
        if (SceneManager.GetActiveScene().name == targetSceneName)
        {
            onReady?.Invoke();
            return;
        }

        StartCoroutine(LoadSceneRoutine(targetSceneName, onReady));
    }

    private IEnumerator LoadSceneRoutine(string targetSceneName, Action onReady)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        onReady?.Invoke();
    }
}
