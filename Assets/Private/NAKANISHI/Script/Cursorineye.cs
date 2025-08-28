using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Cursorineye : MonoBehaviour
{
    [Header("カーソルが目の中にあるか")]
    public bool Cineye;
    [SerializeField] Eyecore eyecore;

    public void OnTriggerStay2D(Collider2D other)//判定
    {
        if (other.gameObject.tag == "eye")//Eyeタグのオブジェクトに触れたら
        {
            eyecore.Cineye = true;
            Cineye = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)//判定
    {
        if (other.gameObject.tag == "eye")//Eyeタグのオブジェクトに触れたら
        {
            eyecore.Cineye = false;
            Cineye = false;
        }
    }
    private void Update()//Eye用Hitbox
    {
        Vector3 mousePosition = Input.mousePosition; //カーソルの位置を取得
        mousePosition.z = 10;
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);//ワールド座標に変換
        transform.position = target;
    }

}





