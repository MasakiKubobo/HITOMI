using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy_move2 : MonoBehaviour
{
    public float turnTime = 2;
    public float SpeedX = 1;

    private float X;
    private float timer = 0;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        X = SpeedX;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= turnTime)
        {
            X *= -1;
            timer = 0;
        }
        rb.AddForce(Vector2.right * X * Time.deltaTime);

        timer += Time.deltaTime;
    }
}
