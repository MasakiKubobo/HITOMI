using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Move : MonoBehaviour
{
    public GameObject player;
    public float Xmax = 50, Xmin = 0, Ymax = 10, Ymin = 0;

    public GameObject gates;

    private CinemachineVirtualCamera vCam;
    private GameObject eye, eyeSP;
    public float eyeShakeTimer, eyeSPShakeTimer;
    public float eyeIntensity, eyeSPIntensity;

    private bool eyeShakeFlag = false, eyeSPShakeFlag = false, gateShakeFlag = false;
    private float eyeTimer = 0, gateTimer = 0;
    private float X, Y;
    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();

        eye = GameObject.Find("eye");
        eyeSP = GameObject.Find("eyeSP");
    }

    // Update is called once per frame
    void Update()
    {
        X = player.transform.position.x;
        Y = player.transform.position.y;

        if (Xmax <= player.transform.position.x) X = Xmax;
        if (Xmin >= player.transform.position.x) X = Xmin;
        if (Ymax <= player.transform.position.y) Y = Ymax;
        if (Ymin >= player.transform.position.y) Y = Ymin;

        transform.position = new Vector3(X, Y, -10);


        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        Eye_Damage eye_Damage = eye.GetComponent<Eye_Damage>();
        EyeSP_Damage eyeSP_Damage = eyeSP.GetComponent<EyeSP_Damage>();

        // 瞳が攻撃を受けた場合、それぞれの振動値を加える
        if (eye_Damage.invincible)
        {
            if (!eyeShakeFlag)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain += eyeIntensity;
                eyeShakeFlag = true;
            }

        }
        if (eyeSP_Damage.invincible)
        {
            if (!eyeSPShakeFlag)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain += eyeSPIntensity;
                eyeSPShakeFlag = true;
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
        if (eyeSPShakeFlag)
        {
            eyeTimer += Time.deltaTime;

            if (eyeTimer >= eyeSPShakeTimer)
            {
                if (!eyeSP_Damage.invincible)
                {
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain -= eyeSPIntensity;
                    eyeTimer = 0;
                    eyeSPShakeFlag = false;
                }
            }
        }


        // ゲートが開き切った時に振動する
        if (gates != null)
        {
            Gate gate = gates.GetComponent<Gate>();
            if (gate.endOpen)
            {

                if (0.1 > gateTimer)
                {
                    if (!gateShakeFlag)
                    {
                        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain += 3;
                        gateShakeFlag = true;
                    }
                }
                else
                {
                    if (gateShakeFlag)
                    {
                        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain -= 3;
                        gateShakeFlag = false;
                    }
                }

                gateTimer += Time.deltaTime;
            }
        }
    }
}
