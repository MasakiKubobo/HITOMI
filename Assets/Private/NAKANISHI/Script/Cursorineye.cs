using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursoreye : MonoBehaviour
{
    public void Cursorineye(Collider other)//判定
    {
        if (other.gameObject.CompareTag("Eye"))//Eyeタグのオブジェクトに触れたら
        {
            Debug.Log("Cineye is true");
            Eyecore eyecore = GetComponent<Eyecore>();
            eyecore.Cineye = true;
        }
        else
        {
            Eyecore eyecore = GetComponent<Eyecore>();
            eyecore.Cineye = false;
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
