using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_bottonSFX : MonoBehaviour
{


    [Header("«öÁä­µ®Ä")]
    public AudioSource sourse;
    public AudioClip hoverSFX;
    public AudioClip clickSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Onhover()
    {
        sourse.PlayOneShot(hoverSFX);
    }

    public void Onclick()
    {
        sourse.PlayOneShot(clickSFX);
    }
}
