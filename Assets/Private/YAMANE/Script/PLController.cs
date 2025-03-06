using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PLController : MonoBehaviour
{
    public LayerMask StageLayer;

    //オブジェクト・コンポーネント参照
    private Rigidbody2D rigidboby2D;
    private SpriteRenderer spriteRenderer;


    //移動関連
    [SerializeField] public float XSpeed;
    [HideInInspector] public float xSpeed;//x方向の速度
    [SerializeField] public float YSpeed;
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
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (Input.GetKey(KeyCode.S) && collision.gameObject.tag == "Ground")
        {
            Debug.Log("Sキーの入力");
            rigidboby2D.AddForce(Vector2.up * (1000 * YSpeed));

            /*if (other.gameObject.tag == "Ground")
            {
                Debug.Log("地面だ＝！！！！！");
            }*/
        }
    }

    private void MoveUpdate()
    {


        if (Input.GetKey(KeyCode.D)) //右方向に移動(右キー取得)
        {
            xSpeed = XSpeed;                    //移動速度の値、右ver
            rightFaching = true;                //右向きのフラグ(ON)
            spriteRenderer.flipX = false;       //スプライトを通常の向きで表示
            Debug.Log("右に移動");
        }
        else if (Input.GetKey(KeyCode.A)) //左方向に移動。左キー取得
        {
            xSpeed = -XSpeed;                    //移動速度の値、左ver
            rightFaching = false;               //右向きのフラグ(OFF)
            spriteRenderer.flipX = true;        //スプライトを左右反転した向きで表示
            Debug.Log("左に移動");
        }
        else                                    //入力がないなら
        {
            xSpeed = 0;                      //停止
        }
    }
    /*
        private void MoveJump()
        {
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rigidboby2D.AddForce(Vector2.up * 300);
                    Debug.Log("ジャンプ");
                }
            }
        }

        private void MoveJum()
        {
            //if (GroundChk())
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("地面");
                rigidboby2D.AddForce(Vector2.up * 300);
                Debug.Log("ジャンプ！");
            }
        }
    */
    private void FixedUpdate()
    {
        Vector2 velocity = rigidboby2D.velocity;//移動速度ベクトルから現在値を取得

        velocity.x = xSpeed;//速度を入力　→　決定
        velocity.y = ySpeed;

        rigidboby2D.velocity = velocity;//計算した移動速度をRigidbodyに反映
    }
    /*
      public bool GroundChk()
      {
          Vector3 startposition = transform.position;
          Vector3 endposition = transform.position - transform.up * 0.3f;

          Debug.DrawLine(startposition, endposition, Color.red);

          return Physics2D.Linecast(startposition, endposition, StageLayer);
      }
  */
}
