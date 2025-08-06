using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class DamageLight : MonoBehaviour
{
    private Light2D HPLight;

    public float currentHP = 100f;

    [Header("最大HP（通常100）")]
    public float maxHP = 100f;

    [Header("HPがこの値以下で赤い縁を表示")]
    public float dangerThreshold = 30f;

    [Header("ライトの参照")]
    public Light2D darkOverlayLight;    // 暗転ライト
    public Light2D damageEdgeLight;     // 赤のライト

    [Header("ライトの最大強度設定")]
    public float maxDarkIntensity = 0.8f;     // 最大暗転の黒ライトの強さ
    public float maxRedEdgeIntensity = 0.8f;  // 赤の最大

    void Start()
    {
        HPLight = GetComponent<Light2D>();
    }
    void Update()
    {
        UpdateLightsBasedOnHP();
    }

    // 現在のHPに応じてライトの明るさを調整する処理。
    void UpdateLightsBasedOnHP()
    {
        // HP比率（0.0 ～ 1.0）
        float hpRatio = Mathf.Clamp01(currentHP / maxHP);

        // 暗転ライトの強さはHPが下がるほど強くなる:0で最大
        // 例: HP100 →: intensity = 0、HP0 → intensity = maxDarkIntensity
        darkOverlayLight.intensity = (1f - hpRatio) * maxDarkIntensity;

        // 赤縁ライトは危険域（dangerThreshold）以下のときに出現する
        if (currentHP < dangerThreshold)
        {
            // 危険度比率（0.0 ～ 1.0）：HPが0に近いほど強く赤くする
            float dangerRatio = 1f - (currentHP / dangerThreshold);
            damageEdgeLight.intensity = dangerRatio * maxRedEdgeIntensity;
        }
        else
        {
            // HPが安全域なら赤いライトは消す
            damageEdgeLight.intensity = 0f;
        }
    }
    /*
        public void SetHP(float hp)
        {
            currentHP = Mathf.Clamp(hp, 0f, maxHP);
        }*/
}

