using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public bool minus;

    public Transform cam;
    public float moveRate;

    private float starpoint;

    void Start()
    {
        starpoint = transform.position.x;
    }


    void Update()
    {
        if (!minus)
        {
            Add();
        }
        if (minus)
        {
            Minus();
        }
    }

    void Add()
    {
        transform.position = new Vector2(starpoint + cam.position.x * moveRate, transform.position.y);
    }

    void Minus()
    {
        transform.position = new Vector2(starpoint - cam.position.x * moveRate, transform.position.y);
    }
}

