using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLController : MonoBehaviour
{
    //オブジェクト・コンポーネント参照
    private Rigidbody2D rigidboby2D;
    private SpriteRenderer spriteRenderer;

    //移動関連
    [HideInInspector] public float xSpeed;//x方向の速度
    [HideInInspector] public float ySpeed;//y方向の速度
    [HideInInspector] public bool rightFaching;//向いている方向(true=右向き/false=左向き)

    void Start()
    {
        rigidboby2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rightFaching = true;//最初に向く方向(true=右向き/false=左向き)
    }

    void Update()
    {
        MoveUpdate();
        MoveJump();
    }

    private void MoveUpdate()
    {
        if (Input.GetKey(KeyCode.D)) //右方向に移動(右キー取得)
        {
            xSpeed = 8.0f;                     //移動速度の値、右ver
            rightFaching = true;                //右向きのフラグ(ON)
            spriteRenderer.flipX = false;       //スプライトを通常の向きで表示
            Debug.Log("右に移動");
        }
        else if (Input.GetKey(KeyCode.A)) //左方向に移動。左キー取得
        {
            xSpeed = -8.0f;                     //移動速度の値、左ver
            rightFaching = false;               //右向きのフラグ(OFF)
            spriteRenderer.flipX = true;        //スプライトを左右反転した向きで表示
            Debug.Log("左に移動");
        }
        else                                    //入力がないなら
        {
            xSpeed = 0.0f;                      //停止
        }
    }

    private void MoveJump()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidboby2D.AddForce(Vector2.up * 300);
            Debug.Log("ジャンプ！");
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rigidboby2D.velocity;//移動速度ベクトルから現在値を取得

        velocity.x = xSpeed;//速度を入力　→　決定
        velocity.y = ySpeed;

        rigidboby2D.velocity = velocity;//計算した移動速度をRigidbodyに反映
    }
}
