using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Move : MonoBehaviour
{
    public float moveSpeed = 1, jumpSpeed = 1;
    public float airMoveSpeed = 5;
    public float disJumpPower;
    private float dashPowor = 0, jumpPower = 0;
    public float jumpLimit = 0.5f;  // ジャンプできる時間

    private bool isJumping = false; // ジャンプの飛翔中か否か
    private bool inAir = true;  // 空中に居るか否か
    private Rigidbody2D rb;
    private float timer = 0;
    private bool jumpFlag = false;

    [HideInInspector] public bool dash, left, jump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inAir) // 着地している場合のみジャンプできる
        {
            dashPowor = moveSpeed;
            if (jump)
            {
                if (!jumpFlag) isJumping = true;
            }
        }
        else dashPowor = airMoveSpeed;

        if (!jump)
        {
            isJumping = false;
            jumpFlag = false;
        }

        if (isJumping)  // ジャンプ中の時間制限
        {
            if (timer >= jumpLimit) isJumping = false;
            jumpPower = jumpSpeed;
            timer += Time.deltaTime;
        }
        else
        {
            jumpPower -= disJumpPower * Time.deltaTime;
            if (jumpPower < 0) jumpPower = 0;
            timer = 0;

            if (inAir) jumpFlag = true;
        }

        PL_Attack pL_Attack = GetComponent<PL_Attack>();
        if (!pL_Attack.attack) // 主人公の向きを反転（アタック中は除く）
        {
            if (left) transform.eulerAngles = new Vector3(0, 180, 0);
            else transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        float xSpeed = 0;
        float ySpeed = 0;

        if (dash) // ダッシュ入力中に
        {
            if (left) // 左向きに走る
            {
                xSpeed = dashPowor;
            }
            else // 右向きに走る
            {
                xSpeed = -dashPowor;
            }
        }

        ySpeed = jumpPower;


        rb.velocity = new Vector2(xSpeed, ySpeed);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            inAir = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            inAir = true;
        }
    }
}
