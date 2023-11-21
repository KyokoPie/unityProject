using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEvent : MonoBehaviour
{
    GameObject player;
    GameObject Boss;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Boss = GameObject.FindGameObjectWithTag("Boss");
    }

    void Update()
    {
        
    }

    public void PlayThrowHelmentAudio()
    {
        AudioManager.BossThrowHelemtAudio();
    }

    public void CloseEventTrigger()
    {
        player.GetComponent<Player_Control>().isEventActive = false;
        Boss.GetComponent<Boss_Controller>().isEvent = false;
        Boss.GetComponent<Animator>().enabled = true;
        Boss.GetComponent<Animator>().SetBool("isAngry", true);
    }
}
