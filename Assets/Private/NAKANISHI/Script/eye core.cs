using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR;

public class eyecore : MonoBehaviour
{
    bool eyeopen = false; //eyeが開いているか
    bool Cinwall = false; //カーソルがwallの中にいるか
    bool Cineye = false; //カーソルがeyeの中にいるか
    void Update()
    {

        Vector3 mousePosition = Input.mousePosition; //カーソルの位置を取得
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);//ワールド座標に変換

        if (eyeopen == false && Cinwall == false) //eyeopen&Cinwallがfalseなら
        {
            if (Input.GetMouseButtonDown(0)) //左クリックを取得
            {
                Invoke(nameof(SummonEye), 0f);
            }

        }
        if (eyeopen == true && Cineye == true) //eyeopneとCineyeがtrueなら
        {
            if (Input.GetMouseButtonDown(0)) //左クリックを取得
            {
                Invoke(nameof(DestroyEye), 0f);
            }
        }

    }

    void SummonEye()
    {
        eyeopen = true; //trueにする
        Debug.Log("eyeopen");
        CancelInvoke(nameof(SummonEye));
    }
    void DestroyEye()
    {
        eyeopen = false; //falseにする
        Debug.Log("eyeclose");
        CancelInvoke(nameof(DestroyEye));
    }
}
