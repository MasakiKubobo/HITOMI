using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class lift_move : MonoBehaviour
{
    [Header("Speed and Direction")]
    public float X;
    public float Y;

    public bool turn = false;
    public float turnTime = 1;

    public bool Parent = true; // 実体化中、触れていると主人公キャラの動きが追従するか

    private Vector2 startPos;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AppearObject appearObject = GetComponent<AppearObject>();
        SwitchAppear switchAppear = GetComponent<SwitchAppear>();

        if (appearObject != null)
        {
            if (appearObject.Materialized) // もし実体化中なら
            {
                if (timer >= turnTime)
                {
                    X *= -1;
                    Y *= -1;
                    timer = 0;
                }
                transform.position += new Vector3(X, Y) * Time.deltaTime;
                if (turn) timer += Time.deltaTime;
            }
            else
            {
                transform.position = startPos;  // 実体化解除でリセット
            }
        }
        else
        {
            if (switchAppear.Materialized) // もし実体化中なら
            {
                if (timer >= turnTime)
                {
                    X *= -1;
                    Y *= -1;
                    timer = 0;
                }
                transform.position += new Vector3(X, Y) * Time.deltaTime;
                if (turn) timer += Time.deltaTime;
            }
            else
            {
                transform.position = startPos;  // 実体化解除でリセット
            }
        }


    }

    void OnCollisionEnter2D(Collision2D other) // リフトの動きにプレイヤーを追従させる
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Parent) other.transform.SetParent(transform); // プレイヤーをリフトの子オブジェクト化
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Parent) other.transform.SetParent(null); // 子オブジェクトから解除
        }
    }
}
