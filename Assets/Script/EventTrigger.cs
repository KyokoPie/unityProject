using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EventTrigger : MonoBehaviour
{

    public PlayableDirector playableDirector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerControl = collision.GetComponent<Player_Control>();
            var playerAnim = collision.GetComponent<Animator>();

            playerControl.isEventActive = true;
            playerAnim.SetFloat("speed", 0f);
            playerAnim.SetFloat("run", 0f);

            playableDirector.Play();

            Destroy(gameObject);

        }

        if (collision.CompareTag("Wolf"))
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            playerObj.GetComponent<Player_Control>().isEventActive = true;
            playerObj.GetComponent<Animator>().SetFloat("speed", 0f);
            playerObj.GetComponent<Animator>().SetFloat("run", 0f);

            playableDirector.Play();

            Destroy(gameObject);


        }
    }
}
