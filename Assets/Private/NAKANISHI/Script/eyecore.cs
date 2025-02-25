using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR;

public class Eyecore : MonoBehaviour
{
    public bool eyeopen = false; //eyeが開いているか
    public bool Cinwall = false; //カーソルがwallの中にいるか
    public bool Cineye = false; //カーソルがeyeの中にいるか
    void Update()
    {

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
        Vector3 mousePosition = Input.mousePosition; //カーソルの位置を取得
        mousePosition.z = 10;
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);//ワールド座標に変換
        //EyeプレハブをGameObject型で取得
        GameObject obj = (GameObject)Resources.Load("Eye");
        //Eyeプレハブを元に、インスタンスを生成、
        Instantiate(obj, target, Quaternion.identity);
        Debug.Log("eyeopen");
    }
    void DestroyEye()
    {
        eyeopen = false; //falseにする
        //eyeタグがついたすべてのオブジェクトを取得
        GameObject[] objects = GameObject.FindGameObjectsWithTag("eye");
        //各オブジェクトを削除
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }

        Debug.Log("eyeclose");

    }
}