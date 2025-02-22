using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class CursorinWall : MonoBehaviour
{
    public void Cursorwall(Collider other)
    {
        bool Cinwall;

        if (other.gameObject.CompareTag("Wall"))
        {
            Cinwall = true;
        }
        else
        {
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
