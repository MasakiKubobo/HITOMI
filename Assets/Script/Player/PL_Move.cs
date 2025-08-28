using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Move : MonoBehaviour
{
    public bool canMove = true;//by山根陸
    public float moveSpeed = 7;
    public float airMoveSpeed = 5;

    public float jumpPower;
    public float disJumpPower; // ジャンプ終わり加える重力補正

    private float dashPowor = 0;
    private float _jumpPower = 0; // 調整して入力する用の変数
    public float maxJumpTime = 0.5f;  // ジャンプできる時間

    public float throwPower = 1; // アイテムを投げる力

    private bool isJumping = false; // ジャンプの飛翔中か否か
    private bool jumpFlag = false; // ジャンプボタン長押しで何回もジャンプしてしまうのを防ぐ
    private bool isGrounded = false;  // 空中に居るか否か
    private bool isHanding = false; // アイテムを掴んでいるか
    private GameObject handObject;
    private bool isStart = true;
    private float jumpTimer, throwTimer;

    private Rigidbody2D rb;

    private PL_Motion pL_Motion;

    public ParticleSystem landing;
    public AudioSource runAudio, landingAudio;
    private bool runFlag, handFlag, throwFlag;

    [HideInInspector] public bool dash, left, jump, hand;
    [HideInInspector] public Vector2 throwPos;
    public GameObject scope;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pL_Motion = GetComponent<PL_Motion>();
        scope.SetActive(false);
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
        if (isStart) transform.eulerAngles = new Vector3(0, 0, 0);
        if (dash) isStart = false;

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

        // アイテムを持ち上げる/投げる
        if (handObject != null)
        {
            if (isHanding)
            {
                if (!hand && !handFlag)
                {
                    handFlag = true;
                }

                if (handFlag)
                {
                    pL_Attack.attack = false;

                    Rigidbody2D handRb = handObject.GetComponent<Rigidbody2D>();
                    handRb.simulated = false;
                    handObject.transform.localPosition = new Vector2(0, 1);

                    if (hand)
                    {
                        dashPowor = 0;
                        throwFlag = true;

                        scope.transform.position = (Vector2)transform.position + throwPos * 1.8f;

                        if (throwTimer < 0.3f)
                        {
                            throwTimer += Time.deltaTime;
                        }
                        else scope.SetActive(true);
                    }

                    if (!hand && throwFlag)
                    {
                        handObject.transform.SetParent(null);
                        handRb.simulated = true;

                        if (throwPos != Vector2.zero && throwTimer >= 0.3f)
                        {
                            handObject.transform.position = scope.transform.position;
                            handRb.AddForce(throwPos * throwPower, ForceMode2D.Impulse);
                        }
                        else
                        {
                            if (left)
                            {
                                handObject.transform.position = (Vector2)transform.position + new Vector2(1, 0);
                            }
                            else
                            {
                                handObject.transform.position = (Vector2)transform.position + new Vector2(-1, 0);
                            }
                        }

                        handObject = null;

                        isHanding = false;
                        handFlag = false;
                        throwFlag = false;
                        throwTimer = 0;
                        scope.SetActive(false);
                    }
                }
            }
        }

    }


    void FixedUpdate()
    {
        if (canMove)
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


            // 可変ジャンプの力の制御
            if (isGrounded && jump) // 接地状態のみジャンプを受け付ける
            {
                if (!jumpFlag)
                {
                    jumpTimer = 0;
                    _jumpPower = jumpPower;
                    ySpeed = _jumpPower;
                    isJumping = true;
                    jumpFlag = true;
                }
                else isJumping = false;
            }
            else if (isGrounded && !jump)
            {
                isJumping = false; // 接地状態でジャンプ入力なし
                jumpFlag = false;
            }
            else if (!isGrounded && !jump)
            {
                jumpFlag = false;
            }

            if (isJumping) // ジャンプ長押し中
            {
                if (jump && jumpTimer <= maxJumpTime) ySpeed = _jumpPower; // ジャンプ入力中、かつ時間内の場合
                else if (!jump || jumpTimer > maxJumpTime) // ジャンプ入力が途切れた、あるいは制限時間が来た場合
                {
                    _jumpPower -= disJumpPower;
                    ySpeed = _jumpPower;

                    if (_jumpPower <= -jumpPower)
                    {
                        _jumpPower = -jumpPower;
                    }
                }
                jumpTimer += Time.deltaTime;
            }
            else jumpTimer = 0;

            if (!isGrounded && !isJumping) ySpeed = -jumpPower;

            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Item"))
        {
            landing.Play();
            landingAudio.Play();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Item"))
        {
            isGrounded = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Item"))
        {
            isGrounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (hand && !isHanding)
            {
                handObject = other.gameObject;
                handObject.transform.SetParent(transform);

                isHanding = true;
                handFlag = false;
            }
        }
    }
}