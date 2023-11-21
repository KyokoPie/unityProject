using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    Button newGameBtn;

    Button quitGameBtn;

    //ItemManager itemManager;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        newGameBtn = transform.GetChild(1).GetComponent<Button>();
        quitGameBtn = transform.GetChild(2).GetComponent<Button>();

        newGameBtn.onClick.AddListener(NewGame);
        quitGameBtn.onClick.AddListener(ExitGame);

        //itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();

        Time.timeScale = 1f;
        Cursor.visible = true;
    }


    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        Cursor.visible = false;
        SceneController.Instance.canUseFireBall = false;

        ItemManager.RestPotion();

        SceneController.Instance.StartGame();

    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    
}
