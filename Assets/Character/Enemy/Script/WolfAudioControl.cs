using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAudioControl : MonoBehaviour
{
    public void PlayYellAudio()
    {
        AudioManager.WolfYellAudio();
    }

    public void PlayWolfAttackAudio()
    {
        AudioManager.WolfAttackAudio();
    }

    public void PlayWolfDeadAudio()
    {
        AudioManager.WolfDeadAudio();
    }
}
