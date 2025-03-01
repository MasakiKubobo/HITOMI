using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitomi_move : MonoBehaviour
{
    public GameObject eye; // 瞳オブジェクト
    public GameObject eyeLight; // 瞳の視野

    [HideInInspector]
    public bool Active = false; // 瞳オブジェクトが非表示か否か
    Vector2 hitomiPos;
    // Start is called before the first frame update
    void Start()
    {
        eye.SetActive(false);
        eyeLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //eyeLight.transform.position = transform.position; // 視野オブジェクトを瞳オブジェクトに追従させる

        // 瞳オブジェクトの2種類のコライダーを取得
        CapsuleCollider2D capsuleCollider = eye.GetComponent<CapsuleCollider2D>(); // 瞳全体の当たり判定
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>(); // 黒目の当たり判定

        if (Input.GetMouseButtonDown(0)) // 画面がクリックされたら
        {
            if (!Active) // 瞳オブジェクトが出現していない場合
            {
                hitomiPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // マウス座標を世界座標に変換
                transform.position = hitomiPos; // マウスの場所に瞳オブジェクトを移動

                eye.SetActive(true);
                eyeLight.SetActive(true);
                Active = true;
            }
            else
            {
                // マウスからRayを飛ばす
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D touch = Physics2D.Raycast(ray.origin, ray.direction);

                if (touch.collider == capsuleCollider || touch.collider == circleCollider) // Rayが瞳オブジェクトに当たったら（すなわち、瞳オブジェクトをクリックしたら）
                {
                    eye.SetActive(false);
                    eyeLight.SetActive(false);
                    Active = false;
                }
                // ↓別の場所をクリックしていたら、瞳オブジェクトを削除して同時にクリックした場所に出現させる
                else
                {
                    hitomiPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = hitomiPos;
                }
                if (touch.collider == null)
                {
                    hitomiPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = hitomiPos;
                }
            }

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            // 出現した際、黒目の届く位置に壁オブジェクトがあれば消えてしまう。
            eye.SetActive(false);
            eyeLight.SetActive(false);
            Active = false;
        }
    }

}
