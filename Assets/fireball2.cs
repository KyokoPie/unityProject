using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball2 : MonoBehaviour
{
    Animator ani;
    AudioSource music;
    public float Speed;
    public float Damge;
    public bool IsBoom;
    private bool hasCollided = false;
    void Awake()
    {
        ani = GetComponent<Animator>();
        music = GetComponent<AudioSource>();

        Speed = 5;
        Damge = 15f;
        IsBoom = false;
    }
    void Start()
    {

    }

    void Update()
    {
        if (!IsBoom)
        {
            Move();
        }
    }
    public void Move()
    {
        transform.position += Speed * Vector3.down * Time.deltaTime;
    }
    public void ChangePosition()
    {
        transform.position = new Vector3(transform.position.x, -3.35f, 0);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            IsBoom = true;
            transform.position = new Vector3(transform.position.x, -1.6f, 0);
            ani.Play("boom");
            music.Play();
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("boom"))
            {
                music.Play();
            }
        }
        else if (collision.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            IsBoom = true;
            transform.position = new Vector3(transform.position.x, -1.6f, 0);
            ani.Play("boom");
            music.Play();
            if (transform.position.x < collision.transform.position.x)
            {
            }
            else if (transform.position.x >= collision.transform.position.x)
            {
            }
        }
    }
    void LateUpdate()
    {
        this.hasCollided = false;
    }
}
