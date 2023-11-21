using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasueMenu : MonoBehaviour
{
    public Button coutinue;
    public Button quit;

    private void Awake()
    {
        coutinue.onClick.AddListener(Coutinue);
        quit.onClick.AddListener(GoBack);
    }

    public void GoBack()
    {
        SceneController.GoBackToMainMenu();
    }

    public void Coutinue()
    {
        UI_Manger.Instance.Continue();
    }
}
