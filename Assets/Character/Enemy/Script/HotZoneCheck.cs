using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private EnemyController enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyController>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("ANIME_Zombie_ATK"))
        {
            enemyParent.Flip();
        }

        if(enemyParent.playerIsDead == true)
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(false);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemyParent.playerIsDead == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                inRange = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
        }
    }
}
