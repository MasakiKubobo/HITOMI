using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class hitomi_view : MonoBehaviour
{
    public float Radius = 5; // 光の届く距離
    public float Angle = 50; // 光とRayが散らばる角度

    [Space(10)]
    public float RayRadius = 4; // Rayの届く距離
    [Range(0, 100)] public int RaysAmount; //Rayの数

    private Light2D light2d;

    [HideInInspector]
    public Vector2 hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        light2d = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Light2Dの各数値の割り当て
        light2d.pointLightOuterRadius = Radius;
        light2d.pointLightOuterAngle = Angle;

        RayScatter();
    }

    // Rayを散らす
    void RayScatter()
    {
        float addAngle = -Angle / 2;

        for (int i = 0; i <= RaysAmount; i++) // RaysAmountの数だけRayを放射する
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction(addAngle), RayRadius, ~LayerMask.GetMask("UI", "Hitomi", "Player"));

            if (hit.collider != null)
            {
                // ヒットした位置まで Ray を描画
                Debug.DrawRay(transform.position, direction(addAngle) * hit.distance);

                RayHitReceiver hitReceiver = hit.collider.GetComponent<RayHitReceiver>();

                // Invisibleタグのみに、以下の処理を加える
                if ((hit.collider.gameObject.CompareTag("Ground") || hit.collider.gameObject.CompareTag("Box")) && hitReceiver != null)
                {
                    hitReceiver.OnRaycastHit();
                }
            }
            else
            {
                // 何もヒットしなかった場合、最大距離まで Ray を描画
                Debug.DrawRay(transform.position, direction(addAngle) * RayRadius);
            }

            addAngle += Angle / RaysAmount; //端から角度を足していき、順番にRayを放射
        }
    }

    // オブジェクトの向き情報をVector2に変換
    Vector2 direction(float addAngle)
    {
        float angle = (transform.eulerAngles.z + 90 + addAngle) * Mathf.Deg2Rad; // Z軸の回転角度をラジアンに変換
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}