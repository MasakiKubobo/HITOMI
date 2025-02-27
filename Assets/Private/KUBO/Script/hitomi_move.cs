using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitomi_move : MonoBehaviour
{
    public GameObject Hitomi, Eye; // 瞳と子オブジェクトの黒目

    bool Active = false; // 瞳オブジェクトが非表示か否か
    Vector2 hitomiPos;
    // Start is called before the first frame update
    void Start()
    {
        Hitomi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 画面がクリックされたら
        {
            if (!Active) // 瞳オブジェクトが出現していない場合
            {
                hitomiPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Hitomi.transform.position = hitomiPos;

                Hitomi.SetActive(true);
                Active = true;
            }
            else
            {
                // マウスからRayを飛ばす
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D touch = Physics2D.Raycast(ray.origin, ray.direction);

                if (touch == Hitomi) // Rayが瞳オブジェクトに当たったら（すなわち、瞳オブジェクトをクリックしたら）
                {
                    Hitomi.SetActive(false);
                    Active = false;
                }
                else // 別の場所をクリックしていたら、瞳オブジェクトを削除して同時にクリックした場所に出現させる
                {
                    hitomiPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Hitomi.transform.position = hitomiPos;
                }
            }

            Debug.Log(hitomiPos);
        }
    }

}
