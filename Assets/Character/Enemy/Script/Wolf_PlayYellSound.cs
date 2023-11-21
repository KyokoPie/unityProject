using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_PlayYellSound : MonoBehaviour
{
    public void PlayYellAudio()
    {
        AudioManager.WolfYellAudio();
    }

    public void PlayWolfMusic()
    {
        AudioManager.PlayWolfMusiclAudio();
    }
}
