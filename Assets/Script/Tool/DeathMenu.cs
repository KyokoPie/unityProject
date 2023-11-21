using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public Button reTry;
    public Button quit;

    private void Awake()
    {
        reTry.onClick.AddListener(ReTry);
        quit.onClick.AddListener(QuitGame);
    }

    public void ReTry()
    {
        SceneController.RestGame();
    }

    public void QuitGame()
    {
        SceneController.GoBackToMainMenu();
    }

}
