using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Control : MonoBehaviour
{
    //Slider healthSlider;
    public Image HP;
    public float AccelerHpSpeed = 0.5f; //���ܳt��

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
        float silderPercent = (float)GameManager.Instance.playerStats.CurrentHealth / GameManager.Instance.playerStats.MaxHealth; //��e��q

        //float OldHP = HP.fillAmount;  //���ܫe����q

        if (Mathf.Abs(silderPercent - HP.fillAmount) < AccelerHpSpeed * Time.deltaTime)
        {
            HP.fillAmount = silderPercent;
        }
        else if (HP.fillAmount > silderPercent) //����
        {
            HP.fillAmount -= AccelerHpSpeed * Time.deltaTime;
        }

        else //�ɦ�
        {
            HP.fillAmount += AccelerHpSpeed * Time.deltaTime;
        }

        //healthSlider.value = silderPercent;
    }

}
