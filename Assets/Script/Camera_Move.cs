using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Move : MonoBehaviour
{
    public GameObject player;
    public float Xmax = 50, Xmin = 0, Ymax = 10, Ymin = 0;

    private CinemachineVirtualCamera vCam;
    private GameObject eye, eyeSP;
    public float eyeShakeTimer, eyeSPShakeTimer;
    public float eyeIntensity, eyeSPIntensity;
    private bool eyeShakeFlag = false, eyeSPShakeFlag = false;

    private float timer = 0;
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

        if (eye_Damage.invincible)
        {
            if (!eyeShakeFlag) eyeShakeFlag = true;
        }
        if (eyeShakeFlag)
        {
            timer += Time.deltaTime;

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = eyeIntensity;
            if (timer >= eyeShakeTimer)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
                if (!eye_Damage.invincible)
                {
                    timer = 0;
                    eyeShakeFlag = false;
                }
            }
        }

        if (eyeSP_Damage.invincible)
        {
            if (!eyeSPShakeFlag) eyeSPShakeFlag = true;
        }
        if (eyeSPShakeFlag)
        {
            timer += Time.deltaTime;

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = eyeSPIntensity;
            if (timer >= eyeSPShakeTimer)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
                if (!eyeSP_Damage.invincible)
                {
                    timer = 0;
                    eyeSPShakeFlag = false;
                }
            }
        }
    }
}
