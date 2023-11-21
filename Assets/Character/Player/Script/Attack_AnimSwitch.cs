using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_AnimSwitch : MonoBehaviour
{
    GameObject playerObj;
    Player_Control playerControl;


    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        playerControl = playerObj.GetComponent<Player_Control>();
    }

    public void Call_Start_Combo()
    {
        playerControl.Start_Combo();
    }

    public void Call_Finish_Animation()
    {
        playerControl.Finish_Animation();
    }

   
    public void Attack01Audio()
    {
        AudioManager.PlayAttack01Audio();
    }
    
    public void RotatingWeaponAudio()
    {
        AudioManager.PlayerRotatingWeaponAudio();
    }

    public void AbsoluteCrit()
    {
        playerControl.AbsoluteCrit();
    }

}
