using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Move : MonoBehaviour
{
    public GameObject player;
    public float Xmax = 50, Xmin = 0;
    private float Ymax, Ymin, X, Y;
    private bool follow = false, center = false;

    public GameObject gates;

    private CinemachineVirtualCamera vCam;
    private GameObject eye;
    public float eyeShakeTimer;
    public float eyeIntensity;

    private bool eyeShakeFlag = false, shakeFlag = false, cwFlag = false;
    private float eyeTimer = 0, shakeTimer = 0, cwTimer = 0, cwTimer2 = 1;
    float cwY, cwX;  // カメラワーク移動用

    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        eye = GameObject.Find("eye");
    }

    // Update is called once per frame
    void Update()
    {
        X = player.transform.position.x;
        Y = player.transform.position.y;

        if (Xmax <= player.transform.position.x) X = Xmax;
        if (Xmin >= player.transform.position.x) X = Xmin;


        if (!follow)  // カメラワークの中にいる場合
        {
            if (Ymax <= player.transform.position.y) Y = Ymax;
            if (Ymin >= player.transform.position.y) Y = Ymin;

            if (!cwFlag)
            {
                cwY = transform.position.y;
                cwTimer = 0;
                cwFlag = true;
            }
            cwTimer += Time.deltaTime * 1.3f;
            Mathf.Clamp01(cwTimer);

            Y = Mathf.Lerp(cwY, Y, cwTimer);
        }
        else          // カメラワークから外れた場合
        {
            if (cwFlag)
            {
                cwY = transform.position.y;
                cwTimer = 0;
                cwFlag = false;
            }
            cwTimer += Time.deltaTime * 1.3f;
            Mathf.Clamp01(cwTimer);

            Y = Mathf.Lerp(cwY, player.transform.position.y, cwTimer);
        }


        if (center)
        {
            cwTimer2 -= Time.deltaTime;
            Mathf.Clamp01(cwTimer2);

            X = Mathf.Lerp(cwX, player.transform.position.x, cwTimer2);
        }


        transform.position = new Vector3(X, Y, -10);




        Eye_Damage eye_Damage = eye.GetComponent<Eye_Damage>();

        // 瞳が攻撃を受けた場合、それぞれの振動値を加える
        if (eye_Damage.invincible)
        {
            if (!eyeShakeFlag)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain += eyeIntensity;
                eyeShakeFlag = true;
            }

        }

        // 瞳の振動時間が経つと元の振動値に戻る（ダメージ演出が続いている限り振動は続く）
        if (eyeShakeFlag)
        {
            eyeTimer += Time.deltaTime;

            if (eyeTimer >= eyeShakeTimer)
            {
                if (!eye_Damage.invincible)
                {
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain -= eyeIntensity;
                    eyeTimer = 0;
                    eyeShakeFlag = false;
                }
            }
        }

    }

    public void Shakes(ref bool shake, float shakeTime, float shakePower)
    {
        if (shake)
        {
            if (shakeTime > shakeTimer)
            {
                if (!shakeFlag)
                {
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain += shakePower;
                    shakeFlag = true;
                }
            }
            else
            {
                if (shakeFlag)
                {
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain -= shakePower;
                    shakeFlag = false;
                    shake = false;
                    shakeTimer = 0;
                }
            }

            shakeTimer += Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("camera"))
        {
            Camera_Work camera_Work = other.GetComponent<Camera_Work>();

            Ymax = camera_Work.Ymax;
            Ymin = camera_Work.Ymin;

            if (camera_Work.isCenter)
            {
                center = true;
                cwX = camera_Work.centerPos.x;
            }
            else center = false;

            follow = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("camera"))
        {
            follow = true;
            center = false;
        }
    }

}
