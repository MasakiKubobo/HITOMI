using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EN03_Move : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private GameObject eye, eyeSP;
    bool inAir = true;
    private GameObject player;
    private bool attackFlag = false;

    EN01_Anim en01_Anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eye = GameObject.Find("eye");
        eyeSP = GameObject.Find("eyeSP");
        player = GameObject.Find("Player");

        en01_Anim = GetComponent<EN01_Anim>();
    }

    // Update is called once per frame
    void Update()
    {
        EyeSP_Move eyeSP_Move = eyeSP.GetComponent<EyeSP_Move>();
        EN01_Damage en01_Damage = GetComponent<EN01_Damage>();


        if (!en01_Damage.knockBack) // ノックバック中でなければ
        {
            if (!inAir && !attackFlag) // 地面にいて攻撃中でなければ
            {
                rb.velocity = new Vector2(transform.localScale.x * moveSpeed, 0);

                en01_Anim.move = true;
            }
            else en01_Anim.move = false;
        }
        else en01_Anim.move = false;

        Debug.Log(inAir);


        // 前方に穴や壁を検知すると反転する
        Vector3 startGround = transform.position + transform.right * 0.5f * transform.localScale.x;
        Vector3 endGround = startGround - transform.up * 0.5f;

        Vector3 startWall = transform.position + transform.right * 0.5f * transform.localScale.x + transform.up * 0.5f;
        Vector3 endWall = startWall + transform.right * 0.5f;

        if (!Physics2D.Linecast(startGround, endGround, LayerMask.GetMask("Ground")) ||
            Physics2D.Linecast(startWall, endWall, LayerMask.GetMask("Ground")))
        {
            if (!en01_Anim.damage)
            {
                if (transform.localScale.x == 1) transform.localScale = new Vector3(-1, 1, 1);
                else if (transform.localScale.x == -1) transform.localScale = new Vector3(1, 1, 1);
            }
        }
        Debug.DrawLine(startGround, endGround, Color.red);
        Debug.DrawLine(startWall, endWall, Color.blue);


        // 瞳に近づくと攻撃アニメーションに入る
        Vector2 moveDirection = !eyeSP_Move.appear ? eye.transform.position - transform.position : eyeSP.transform.position - transform.position;

        Vector3 targetPos = eyeSP_Move.appear ? eyeSP.transform.position : eye.transform.position;



        if (0.8 >= Mathf.Abs(moveDirection.x) && 3 >= Mathf.Abs(moveDirection.y))
        {
            if (!attackFlag)
            {
                // 攻撃前に瞳の方を向く
                if (moveDirection.x >= 0.5)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (moveDirection.x <= -0.5)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                en01_Anim.attack = true;
                attackFlag = true;
            }
        }
        else attackFlag = false;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) inAir = false;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        //if (other.gameObject.CompareTag("Ground")) inAir = true;
    }
}
