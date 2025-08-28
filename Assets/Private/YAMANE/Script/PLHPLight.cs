

using UnityEngine;
using UnityEngine.Rendering.Universal; // Universal Render Pipeline (URP) の Light 2D を使用する場合に必要

public class LightControl : MonoBehaviour
{
    public Light2D hpLight; // インスペクターからHpLightオブジェクトにアタッチされているLight2Dコンポーネントを割り当てる
    public float changeAmount = 100f; // Falloffを変更する量 (ここを100に設定)
    public float minRadius = 0f; // Outer Radiusの最小値
    public float maxRadius = 3000f; // Outer Radiusの最大値 (必要に応じて調整 - 1136よりも十分大きく)


    void Start()
    {
        // もしhpLightが設定されていなければ、このゲームオブジェクト自身からLight2Dコンポーネントを取得しようとする
        if (hpLight == null)
        {
            hpLight = GetComponent<Light2D>();
            if (hpLight == null)
            {
                Debug.LogError("Light2Dコンポーネントが見つかりません。HpLightオブジェクトにこのスクリプトをアタッチするか、手動でhpLight変数を設定してください。", this);
                enabled = false; // スクリプトを無効にする
                return;
            }
        }
    }

    void Update()
    {


        // Aキーが押されたらFalloffを小さくする
        if (Input.GetKeyDown(KeyCode.A))
        {
            hpLight.shapeLightFalloffSize -= changeAmount;
            /*
            if (hpLight != null)
            {

            
                // ここを hpLight.pointLightOuterRadius に変更
                hpLight.pointLightOuterRadius = Mathf.Max(hpLight.pointLightOuterRadius - changeAmount, minRadius);
                Debug.Log("Outer Radius (Aキー): " + hpLight.pointLightOuterRadius);
        } */
        }
        ;

        // Dキーが押されたらFalloffを大きくする
        if (Input.GetKeyDown(KeyCode.D))
        {

            hpLight.shapeLightFalloffSize += changeAmount;
            /*
            if (hpLight != null)
            {
                // ここを hpLight.pointLightOuterRadius に変更
                hpLight.pointLightOuterRadius = Mathf.Min(hpLight.pointLightOuterRadius + changeAmount, maxRadius);
                Debug.Log("Outer Radius (Dキー): " + hpLight.pointLightOuterRadius);
            }*/
        }
        ;
    }
}