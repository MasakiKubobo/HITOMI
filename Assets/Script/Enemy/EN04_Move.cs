using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EN04_Move : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject eye, eyeSP;
    bool inAir = true;

    public float plDistanceX = 10, plDistanceY = 5;
    private GameObject player;
    [HideInInspector] public bool attackMode = false;   // 瞳を攻撃する
    public bool isEnemy05 = false;

    public bool targetIsPlayer = false; // ターゲットをプレイヤーにするか
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eye = GameObject.Find("eye");
        player = GameObject.Find("Player");

        if (targetIsPlayer)
        {
            eye = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {

        // 範囲内にプレイヤーが入ると近づいてくる
        if (plDistanceX >= Math.Abs(transform.position.x - player.transform.position.x) &&
            plDistanceY >= Math.Abs(transform.position.y - player.transform.position.y))
        {
            if (!isEnemy05) attackMode = true;
        }
        else
        {
            attackMode = false;
        }

    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) inAir = false;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) inAir = true;
    }
}
