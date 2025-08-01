using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Move : MonoBehaviour
{
    public float moveSpeed = 7;
    public float airMoveSpeed = 5;

    public float startJumpPower, loopJumpPower; // 最初に加える力と、継続して加える力 
    public float disJumpPower; // ジャンプ終わり加える重力補正

    private float dashPowor = 0;
    private float _loopJumpPower = 0; // 調整して入力する用の変数
    public float maxJumpTime = 0.5f;  // ジャンプできる時間

    private bool isJumping = false; // ジャンプの飛翔中か否か
    private bool isGrounded = false;  // 空中に居るか否か
    private float jumpTimer = 0;

    private Rigidbody2D rb;

    private PL_Motion pL_Motion;

    public ParticleSystem landing;
    public AudioSource runAudio, landingAudio;
    private bool runFlag;

    [HideInInspector] public bool dash, left, jump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pL_Motion = GetComponent<PL_Motion>();
    }

    // Update is called once per frame
    void Update()
    {
        // ダッシュに加える力の制御
        if (isGrounded) dashPowor = moveSpeed;
        else dashPowor = airMoveSpeed;

        PL_Attack pL_Attack = GetComponent<PL_Attack>();
        if (!pL_Attack.attack) // 主人公の向きを反転（アタック中は除く）
        {
            float Z = transform.eulerAngles.z;
            if (left) transform.eulerAngles = new Vector3(0, 0, Z);
            else transform.eulerAngles = new Vector3(0, 180, Z);
        }


        if (isGrounded && dash)
        {
            pL_Motion.dash = true;
            if (!runFlag)
            {
                runAudio.Play();
                runFlag = true;
            }
        }
        else
        {
            pL_Motion.dash = false;
            runAudio.Stop();
            runFlag = false;
        }

        if (!isGrounded) pL_Motion.jumpDown = true;
        else pL_Motion.jumpDown = false;

    }


    void FixedUpdate()
    {
        float xSpeed = 0;

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

        rb.velocity = new Vector2(xSpeed, rb.velocity.y);


        // 可変ジャンプの力の制御
        if (isGrounded && jump) // 接地状態のみジャンプを受け付ける
        {
            if (!isJumping)
            {
                rb.AddForce(Vector2.up * startJumpPower, ForceMode2D.Impulse);
                isJumping = true;
            }
        }
        else if (isGrounded && !jump) isJumping = false;

        if (isJumping) // ジャンプ長押し中
        {
            if (jump && jumpTimer <= maxJumpTime)
            {
                _loopJumpPower = loopJumpPower;
            }
            else if (!jump || jumpTimer > maxJumpTime) // ジャンプ入力が途切れた、あるいは制限時間が来た場合
            {
                _loopJumpPower -= disJumpPower;
                if (_loopJumpPower <= 0)
                {
                    _loopJumpPower = 0;
                }
            }

            rb.AddForce(Vector2.up * _loopJumpPower, ForceMode2D.Force);
            jumpTimer += Time.deltaTime;
        }
        else jumpTimer = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            landing.Play();
            landingAudio.Play();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}