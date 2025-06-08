using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PLHPLight : MonoBehaviour
{

    [Header("プレイヤーHP設定")]
    [Range(0f, 100f)]
    public float currentHP = 100f;

    [Header("ライト参照")]
    public Light2D HPLight;      // 暗転ライト（黒）
    public Light2D RedLight;     // 赤縁ライト（画面端）
    public Light2D GlobalLight;  // 全体の淡い照明

    void Update()
    {
        Debug.Log($"Current HP:{currentHP}");

        // デバッグ入力：Aキーで5ダメージ
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentHP -= 5f;
            currentHP = Mathf.Clamp(currentHP, 0f, 100f);
            Debug.Log($"Damage: HP = {currentHP}");
            Debug.Log("ダメージ");
        }

        // デバッグ入力：Dキーで10回復
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentHP += 10f;
            currentHP = Mathf.Clamp(currentHP, 0f, 100f);
            Debug.Log($"Heal: HP = {currentHP}");
            Debug.Log("回復");
        }

        UpdateHPLight();
        UpdateRedLight();
    }

    void UpdateHPLight()
    {
        if (currentHP <= 50f)
        {
            float t = Mathf.Clamp01((50f - currentHP) / 50f);
            HPLight.intensity = t;
        }
        else
        {
            HPLight.intensity = 0f;
        }
    }

    void UpdateRedLight()
    {
        if (currentHP <= 20f)
        {
            float t = Mathf.Clamp01((20f - currentHP) / 20f);
            RedLight.intensity = t;
        }
        else
        {
            RedLight.intensity = 0f;
        }
    }
}