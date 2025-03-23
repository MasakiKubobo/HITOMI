using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitomi_eyeMove : MonoBehaviour
{
    public GameObject eye_black; // 黒目オブジェクト
    public GameObject eyeLight; // 瞳の視野
    public float eyeShrink = 0.8f; // 黒目の大きさの倍率（この数値は黒目を小さくするのに使う）
    public float Radius = 0.5f; // 黒目が回転する半径
    // Start is called before the first frame update
    void Start()
    {
        eyeLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>(); // 黒目を動かす用の当たり判定を取得

        eye_black.transform.localScale = new Vector2(eyeShrink, eyeShrink); // 黒目を小さくする

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // カーソルの位置を取得
        Vector2 direction = (cursorPos - (Vector2)transform.position).normalized; // カーソルの方向を取得し正規化
        eye_black.transform.localPosition = direction * Radius; // 半径を決めて黒目をカーソルの方向へ回転させる

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 角度を計算
        eyeLight.transform.rotation = Quaternion.Euler(0, 0, angle - 90); // カーソルの方向にライトを回転させる


        hitomi_move hitomiMove = GetComponent<hitomi_move>();
        if (hitomiMove.gameOver)
        {
            eye_black.transform.localPosition = new Vector2(0, 0);
            eye_black.transform.localScale = new Vector2(1.2f, 1.2f);
        }


        // マウスからRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D touch = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, ~LayerMask.GetMask("UI"));

        if (touch.collider == circleCollider) // 瞳の中央に触れていたら、黒目の位置は中心に戻り、大きさも元に戻る
        {
            eyeLight.SetActive(false);
            eye_black.transform.localPosition = new Vector2(0, 0);
            eye_black.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            if (hitomiMove.Active) eyeLight.SetActive(true); // 瞳全体が非表示でなければ視野オブジェクトも表示する
        }
    }
}
