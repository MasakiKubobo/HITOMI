using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int maxJumpCount = 2;

    private Rigidbody2D rb;
    private int jumpCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = 0f;

        // 左右移動
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;
        }

        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // ジャンプ（WキーまたはShiftキー）
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // ジャンプ直前にY速度をリセット
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }
}