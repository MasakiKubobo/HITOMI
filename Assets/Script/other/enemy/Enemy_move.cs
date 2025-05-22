using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_move : MonoBehaviour
{
    public float sleepTime = 0.5f;
    public float Speed = 1;

    public GameObject Hitomi;

    private Vector2 startPos;
    private float timer = 0;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemy_appear enemyAppear = GetComponent<Enemy_appear>();
        if (enemyAppear.Materialized) // もし実体化中なら
        {
            if (timer >= sleepTime)
            {
                Vector2 direction = Hitomi.transform.position - transform.position;
                rb.AddForce(direction * Speed * Time.deltaTime);
            }
            timer += Time.deltaTime;
        }
        else
        {
            transform.position = startPos;  // 実体化解除でリセット
            timer = 0;
        }
    }
}
