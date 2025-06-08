using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EyeSP_Move : MonoBehaviour
{
    public Light2D HpLight;
    public GameObject kurome;
    private Light2D eyeLight;

    [HideInInspector] public Vector2 kuromePos;
    [HideInInspector] public bool appear;

    private float coolDownTimer = 0; // Rスティックの不感時間
    // Start is called before the first frame update
    void Start()
    {
        eyeLight = kurome.GetComponent<Light2D>();
        eyeLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Sqrt(kuromePos.x * kuromePos.x + kuromePos.y * kuromePos.y) > 0.8) // スティックの傾きが最大で有効に
        {
            eyeLight.enabled = true;

            //if (coolDownTimer >= 0.1)
            //{
            kurome.transform.localPosition = kuromePos * 0.2f;

            // 逆三角関数を用いてVector2から角度の数値を入力
            kurome.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(kuromePos.y, kuromePos.x) * Mathf.Rad2Deg - 90);
            //}
            coolDownTimer += Time.deltaTime;
        }
        else coolDownTimer = 0;


        if (!appear)
        {
            HpLight.intensity = 1;
            eyeLight.enabled = false;
            kurome.transform.localPosition = Vector2.zero;
        }
        else HpLight.intensity = 0.4f;
    }
}
