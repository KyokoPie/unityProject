using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_HP_Contriller : MonoBehaviour
{
    public Image HP;
    public float AccelerHpSpeed = 0.5f;

    GameObject Boss;
    CharacterStats characterStats;

    private void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        characterStats = Boss.GetComponent<CharacterStats>();
        print(Boss);
    }

    private void Update()
    {
        UpdataHealth();
    }

    void UpdataHealth()
    {
        float silderPercent = (float)characterStats.CurrentHealth / characterStats.MaxHealth; //��e��q

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
