using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallatt : MonoBehaviour
{
    GameObject Boss;
    Animator animator;
    Collider2D collider2d;

    Vector3 dir;

    public float Speed;

    public float Damge;

    public float LifeTime;
    private bool hasCollided = false;

    void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        print(Boss);
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();

        dir = transform.localScale;

        Speed = 5;

        Damge = 8f;

        LifeTime = 7f;
    }

    void Update()
    {
        Move();
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0 || Boss.GetComponent<Boss_Controller>().isDied)
        {
            Destroy(gameObject);
        }
    }
    public void Move()
    {
        if (Boss.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-dir.x, dir.y, dir.z);
            transform.position += Speed * -transform.right * Time.deltaTime;
        }
        else if (Boss.transform.localScale.x > 0) 
        {
            transform.localScale = new Vector3(dir.x, dir.y, dir.z);
            transform.position += Speed * transform.right * Time.deltaTime;
        }
    }
    public void CloseCollider() 
    {
        collider2d.enabled = false;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            if (this.hasCollided == true) { return; }
            this.hasCollided = true;
            animator.Play("Hit");
            if (transform.position.x < collision.transform.position.x)
            {
            }
            else if (transform.position.x >= collision.transform.position.x)
            {
            }

        }
        else if (collision.CompareTag("Ground")) 
        {
            animator.Play("Hit");
        }
    }
    void LateUpdate()
    {
        this.hasCollided = false;
    }
}
