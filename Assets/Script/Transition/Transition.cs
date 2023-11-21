using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public bool isSave;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isSave)
            {
                SaveManager.Instance.SavePlayerData();
                print("GameSave");
            }

            ItemManager.SavePotion();

            SceneController.Instance.GoToNextScene();

            gameObject.SetActive(false);
        }
    }
}
