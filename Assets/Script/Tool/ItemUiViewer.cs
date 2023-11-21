using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUiViewer : MonoBehaviour
{
    public Text hp_Amount;
    public Text stamina_Amount;

    private void Update()
    {
        DisplayPotion();
    }

    void DisplayPotion()
    {
        hp_Amount.text = ItemManager.Instance.hpPotion_Amount.ToString();
        stamina_Amount.text = ItemManager.Instance.staminaPotion_Amount.ToString();
    }
}
