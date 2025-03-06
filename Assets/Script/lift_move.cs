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

    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AppearObject appearObject = GetComponent<AppearObject>();
        if (appearObject.Materialized) // もし実体化中なら
        {
            transform.position += new Vector3(X, Y) * Time.deltaTime;
        }
        else
        {
            transform.position = startPos;  // 実体化解除でリセット
        }
    }

    void OnCollisionEnter2D(Collision2D other) // リフトの動きにプレイヤーを追従させる
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform); // プレイヤーをリフトの子オブジェクト化
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null); // 子オブジェクトから解除
        }
    }
}
