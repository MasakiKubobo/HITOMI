using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class CursorinWall : MonoBehaviour
{

    [Header("カーソルが壁の中にあるか")]
    public bool Cinwall;
    [SerializeField] Eyecore eyecore;

    public void OnTriggerStay2D(Collider2D other)//判定
    {
        if (other.gameObject.tag == "wall")//wallタグのオブジェクトに触れたら
        {
            eyecore.Cinwall = true;
            Cinwall = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)//判定
    {
        if (other.gameObject.tag == "wall")//wallタグのオブジェクトに触れたら
        {
            eyecore.Cinwall = false;
            Cinwall = false;
        }
    }
    private void Update() //Wall用ヒットボックス
    {
        Vector3 mousePosition = Input.mousePosition; //カーソルの位置を取得
        mousePosition.z = 10;
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);//ワールド座標に変換
        transform.position = target;
    }



}
