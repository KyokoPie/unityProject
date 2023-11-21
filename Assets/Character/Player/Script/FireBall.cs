using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float damage;
    public float speed;
    public float face;
    public float destoryDistance;

    private Vector3 startPos;


    void Start()
    {
        damage = GameManager.Instance.playerStats.attackData.magicDamge;
        startPos = transform.position;
    }

    void Update()
    {
        float disstance = (transform.position - startPos).sqrMagnitude;
        if(disstance > destoryDistance)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * face * Time.deltaTime);

    }
}
