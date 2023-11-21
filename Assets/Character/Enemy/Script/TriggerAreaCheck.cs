using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private EnemyController enemyParent;
    private WolfController wolfPraent;


    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (enemyParent.playerIsDead == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                enemyParent.target = collision.transform;
                enemyParent.inRange = true;
                enemyParent.hotZone.SetActive(true);
            }
        }        
    }
}
