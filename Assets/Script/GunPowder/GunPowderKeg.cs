using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPowderKeg : MonoBehaviour
{

    public GameObject explosionPrefab;
    public GameObject explosionObj;

    public bool isEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBall"))
        {
            if (!isEvent)
            {
                Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }
            else
            {
                explosionObj.SetActive(true);
            }
            CameraShake.Instance.ShakeCamera(5f, 0.1f);
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }

    }
}
