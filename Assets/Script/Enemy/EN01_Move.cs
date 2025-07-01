using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EN01_Move : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private GameObject eye, eyeSP;
    bool inAir = true;

    public float plDistanceX = 10, plDistanceY = 5;
    private GameObject player;
    private bool attackFlag = false;
    private bool autoChase = true;   // プレイヤーを追従する

    public bool targetIsPlayer = false; // ターゲットをプレイヤーにするか

    EN01_Anim en01_Anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eye = GameObject.Find("eye");
        eyeSP = GameObject.Find("eyeSP");
        player = GameObject.Find("Player");

        if (targetIsPlayer)
        {
            eye = GameObject.Find("Player");
            eyeSP = GameObject.Find("Player");
        }

        en01_Anim = GetComponent<EN01_Anim>();
    }

    // Update is called once per frame
    void Update()
    {
        EyeSP_Move eyeSP_Move = eyeSP.GetComponent<EyeSP_Move>();
        EN01_Damage en01_Damage = GetComponent<EN01_Damage>();

        // 範囲内にプレイヤーが入ると近づいてくる
        if (plDistanceX >= Math.Abs(transform.position.x - player.transform.position.x) &&
            plDistanceY >= Math.Abs(transform.position.y - player.transform.position.y))
        {
            autoChase = true;
            en01_Anim.move = true;
        }
        else
        {
            autoChase = false;
            en01_Anim.move = false;
        }

        float moveDirection;
        if (!targetIsPlayer)
        {
            moveDirection = !eyeSP_Move.appear ? eye.transform.position.x - transform.position.x : eyeSP.transform.position.x - transform.position.x;
        }
        else
        {
            moveDirection = player.transform.position.x - transform.position.x;
        }

        if (moveDirection >= 0.5)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection <= -0.5)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // eyeとeyeSPのどちらかの座標を反映し、左右どちらかに正規化する
        if (moveDirection >= 0)
        {
            moveDirection = 1;
        }
        else
        {
            moveDirection = -1;
        }


        if (!en01_Damage.knockBack) // ノックバック中でなければ
        {
            if (autoChase && !inAir)
            {
                rb.velocity = new Vector2(moveDirection * moveSpeed, 0); // 瞳に近づく

                en01_Anim.move = true;
            }
            else en01_Anim.move = false;
        }
        else en01_Anim.move = false;


        // 瞳に近づくと攻撃アニメーションに入る
        if (!targetIsPlayer)
        {
            Vector3 targetPos = eyeSP_Move.appear ? eyeSP.transform.position : eye.transform.position;

            if (3 >= (transform.position - targetPos).magnitude)
            {
                if (!attackFlag)
                {
                    en01_Anim.attack = true;
                    attackFlag = true;
                }
            }
            else attackFlag = false;
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
