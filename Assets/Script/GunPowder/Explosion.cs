using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Explosion : MonoBehaviour
{
    public float lifeTime;
    public bool isSkill;
    public PlayableDirector playableDirector;
    void Start()
    {
        AudioManager.ExplosionAudio();
        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSkill)
        {
            if (collision.CompareTag("Player"))
            {
                GameManager.Instance.playerStats.characterData.currentHealth -= 30f;
            }
        }
        

        if (collision.CompareTag("Enemy"))
        {
            var target = collision.GetComponent<CharacterStats>();
            target.characterData.currentHealth -= 30f;
        }

        if (collision.CompareTag("Wolf"))
        {
            playableDirector.Play();
        }
    }

}
