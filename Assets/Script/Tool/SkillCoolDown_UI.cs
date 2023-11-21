using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDown_UI : MonoBehaviour
{
    Player_Control player_Control;

    public Image fillImage;

    float cooldownTime = 3f;
    float fireballTimer = 0f;
    bool startFireballCoolDown;
    bool isEvent;

    private void Start()
    {
        player_Control = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
        fillImage = transform.Find("FireSkill_Background").GetComponent<Image>();
        cooldownTime = player_Control.cooldownTime;
    }

    private void Update()
    {
        isEvent = player_Control.isEventActive;

        if (!isEvent)
        {
            startFireballCoolDown = player_Control.startFireballCoolDown;

            if (startFireballCoolDown)
            {
                fireballTimer += Time.deltaTime;

                fillImage.fillAmount = (cooldownTime - fireballTimer) / cooldownTime;

                if (fireballTimer >= cooldownTime)
                {
                    fillImage.fillAmount = 0;
                    fireballTimer = 0f;
                    startFireballCoolDown = false;
                }
            }
        }        

        
    }

}
