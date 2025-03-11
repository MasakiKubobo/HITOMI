using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PL_move : MonoBehaviour
{
    public float Speed; // ダッシュの速度
    public float skySpeed; // 空中時の速度
    public float jumpPower; // ジャンプ力

    private bool canJump = false; // ジャンプ可能か
    private bool hold = false; // 何かを持っているか
    private GameObject Item;
    private Rigidbody2D rb, Rb;
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

        if (hold)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Item.transform.SetParent(null);
                Rb.isKinematic = false;
                hold = false;
            }

        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal"); // 左右の入力を取得

        if (canJump) rb.velocity = new Vector2(x * Speed, rb.velocity.y); // ダッシュ出力
        else rb.velocity = new Vector2(x * skySpeed, rb.velocity.y); // 空中のダッシュ出力
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Debug.Log("ゲームオーバー");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Item = other.gameObject;
                other.transform.SetParent(transform);
                Rb = other.gameObject.GetComponent<Rigidbody2D>();
                Rb.isKinematic = true;
                hold = true;
            }
        }
    }

    void OnCollisionStay2D(Collision2D other) // 地面に足がついている時だけジャンプ可能
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }

        if (other.gameObject.CompareTag("Item"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Item = other.gameObject;
                other.transform.SetParent(transform);
                Rb = other.gameObject.GetComponent<Rigidbody2D>();
                Rb.isKinematic = true;
                hold = true;
            }
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
