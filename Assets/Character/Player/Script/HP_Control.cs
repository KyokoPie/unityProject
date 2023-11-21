using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Control : MonoBehaviour
{
    //Slider healthSlider;
    public Image HP;
    public float AccelerHpSpeed = 0.5f; //漸變速度

    private void Awake()
    {
        //healthSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        UpdataHealth();
    }

    void UpdataHealth()
    {
        float silderPercent = (float)GameManager.Instance.playerStats.CurrentHealth / GameManager.Instance.playerStats.MaxHealth; //當前血量

        //float OldHP = HP.fillAmount;  //漸變前的血量

        if (Mathf.Abs(silderPercent - HP.fillAmount) < AccelerHpSpeed * Time.deltaTime)
        {
            HP.fillAmount = silderPercent;
        }
        else if (HP.fillAmount > silderPercent) //扣血
        {
            HP.fillAmount -= AccelerHpSpeed * Time.deltaTime;
        }

        else //補血
        {
            HP.fillAmount += AccelerHpSpeed * Time.deltaTime;
        }

        //healthSlider.value = silderPercent;
    }

}
