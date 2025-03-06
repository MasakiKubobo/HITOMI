using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class PL_move : MonoBehaviour
{
    public float Speed; // ダッシュの速度
    public float skySpeed; // 空中時の速度
    public float jumpPower; // ジャンプ力

    private bool canJump = false; // ジャンプ可能か
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canJump && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // ジャンプ出力
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal"); // 左右の入力を取得

        rb.velocity = new Vector2(x * Speed, rb.velocity.y); // ダッシュ出力
    }

    void OnCollisionStay2D(Collision2D other) // 地面に足がついている時だけジャンプ可能
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
    void OnCollisionExit2D(Collision2D other) // 地面が足が離れた時はジャンプ不可にする
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}
