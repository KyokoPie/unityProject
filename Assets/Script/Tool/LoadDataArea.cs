using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemManager.LoadPotion();
            SaveManager.Instance.LoadPlayerData();
            print("LoadGame");
            gameObject.SetActive(false);
        }
    }
}
