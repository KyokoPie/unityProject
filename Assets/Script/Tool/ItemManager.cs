using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : Singleton<ItemManager>
{
    public int hpPotion_Amount;
    public int hpPotion_Current_Amount;
    public int staminaPotion_Amount;
    public int staminaPotion_Current_Amount;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        hpPotion_Amount = 2;
        staminaPotion_Amount = 2;
    }

    public static void SavePotion()
    {
        Instance.hpPotion_Current_Amount = Instance.hpPotion_Amount;
        Instance.staminaPotion_Current_Amount = Instance.staminaPotion_Amount;
    }

    public static void LoadPotion()
    {
        if(Instance.hpPotion_Current_Amount == 0)
        {
            Instance.hpPotion_Amount = 2;
        }
        else
        {
            Instance.hpPotion_Amount = Instance.hpPotion_Current_Amount + 1;
        }

        if (Instance.staminaPotion_Current_Amount == 0)
        {
            Instance.staminaPotion_Amount = 2;
        }
        else
        {
            Instance.staminaPotion_Amount = Instance.staminaPotion_Current_Amount + 1;
        }
    }

    public static void RestPotion()
    {
        Instance.staminaPotion_Amount = 2;
        Instance.hpPotion_Amount = 2;
        Instance.hpPotion_Current_Amount = 0;
        Instance.staminaPotion_Current_Amount = 0;
    }

}
