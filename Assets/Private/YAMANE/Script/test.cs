using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float gravity = 0;
    public GroundCheck ground;
    public float jumpHeight;
    public float jumpPos;

    private bool dash = false;
    private bool jump = false;
    private bool isjump = false;
    private bool isGround = false;

    private string GroundTag = "Ground";

    [SerializeField] public float PL_speed = 10.0f;
    [HideInInspector] public float Xspeed;

    [SerializeField] public float PL_jump = 30.0f;
    [HideInInspector] public float Yspeed;
    private Rigidbody2D rb2D;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            dash = true;
            Debug.Log("dashにtrueを代入");
        }
        else
        {
            dash = false;
        }

        if (isGround == true)
        {
            jump = true;
            Debug.Log("jumpにtrueを代入");
        }
        else
        {
            jump = false;
            Debug.Log("jumpにfalseを代入");
        }




        //***********↓↓↓↓↓↓左右移動の処理↓↓↓↓↓↓**************************
        if (dash == true && Input.GetKey(KeyCode.D))
        {
            Xspeed = PL_speed;
            //Debug.Log("dash=D");
        }
        if (dash == true && Input.GetKey(KeyCode.A))
        {
            Xspeed = -PL_speed;
            //Debug.Log("dash=A");
        }
        if (dash == false)
        {
            Xspeed = 0.0f;
            Debug.Log("dash=false");
        }
        //***********↑↑↑↑↑↑左右移動の処理↑↑↑↑↑↑**************************

        //***********↓↓↓↓↓↓ジャンプの処理↓↓↓↓↓↓**************************
        if (isGround == true)
        {
            if (jump == true && Input.GetKey(KeyCode.W))
            {
                Yspeed = PL_jump;//上方向への加速
                //jumpPos = transform.position.y;
                isjump = true;
                Debug.Log("Wキーが押された");
            }
            else
            {
                Yspeed = gravity;//重力　した方向の速度
                isjump = false;
            }
        }
        /*
        else if (isjump)
        {
            if (jump == true && Input.GetKey(KeyCode.W) && jumpPos + jumpHeight > transform.position.y)
            {
                Yspeed = PL_jump;
            }
            else
            {
                isjump = false;
            }

        }*/

        //***********↑↑↑↑↑↑ジャンプの処理↑↑↑↑↑↑**************************
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == GroundTag)
        {
            isGround = true;
            Debug.Log("ground接触");
        }
    }

    private void FixedUpdate()
    {
        //isGround = ground.IsGround();
        //Yspeed = -gravity;//重力　した方向の速度

        Vector2 velosity = rb2D.velocity;
        //velosity.x = Xspeed;
        //velosity.y = Yspeed;

        //rb2D.velocity = velosity;
        rb2D.velocity = new Vector2(Xspeed, Yspeed);//追加した
    }
}
