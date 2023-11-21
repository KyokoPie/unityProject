using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{

    public GameObject sceneFaderPrefab;
    public bool canUseFireBall;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            canUseFireBall = true;
        }
    }
    public void GoToNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;              
        StartCoroutine(LoadLevel(index + 1));

    }

    public static void GoToPreviousScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(index - 1);
    }
    
    public static void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);        
    }   
    
    public static void RestGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.PlayMusicAudio();
    }

    public void StartGame()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(index + 1));

    }

    IEnumerator LoadLevel(int scene)
    {
        sceneFaderPrefab.SetActive(true);
        SceneFader fader = sceneFaderPrefab.GetComponent<SceneFader>();
        yield return StartCoroutine(fader.FadeOut(1f));        
        yield return SceneManager.LoadSceneAsync(scene);
        yield return StartCoroutine(fader.FadeIn(1f));
        sceneFaderPrefab.SetActive(false);
        yield break;        
    }
}
