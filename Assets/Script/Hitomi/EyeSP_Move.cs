using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EyeSP_Move : MonoBehaviour
{
    public Light2D HpLight;
    public GameObject kurome;
    private Light2D eyeLight;
    public GameObject effect;
    public GameObject ber;
    public float attackTime = 3;
    private float attackTimer;

    public AudioSource openAudio, attackAudio;
    private bool openFlag, attackFlag;

    [HideInInspector] public Vector2 kuromePos;
    [HideInInspector] public bool appear;

    // Start is called before the first frame update
    void Start()
    {
        eyeLight = kurome.GetComponent<Light2D>();
        eyeLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider = kurome.GetComponent<Collider2D>();
        if (Math.Sqrt(kuromePos.x * kuromePos.x + kuromePos.y * kuromePos.y) > 0.8) // スティックの傾きが最大で有効に
        {
            eyeLight.enabled = true;
            kurome.transform.position = (Vector2)transform.position + kuromePos * 0.2f;

            // 逆三角関数を用いてVector2から角度の数値を入力
            kurome.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(kuromePos.y, kuromePos.x) * Mathf.Rad2Deg - 90);
            collider.enabled = true;
        }

        if (!appear)
        {
            HpLight.intensity = 1;
            collider.enabled = false;
            eyeLight.enabled = false;
            kurome.transform.localPosition = Vector2.zero;

            openFlag = false;
        }
        else
        {
            HpLight.intensity = 0.4f;

            if (!openFlag)
            {
                openAudio.Play();
                openFlag = true;
            }

            if (PL_Controller.usingItem)
            {
                eyeLight.color = new Color(0, 0, 1, 1);
                effect.SetActive(true);
                ber.SetActive(true);

                if (!attackFlag)
                {
                    attackAudio.Play();
                    attackFlag = true;
                }


                Slider slider = ber.GetComponent<Slider>();
                slider.value = (attackTime - attackTimer) / attackTime;

                attackTimer += Time.deltaTime;
                if (attackTimer >= attackTime)
                {
                    PL_Controller.usingItem = false;
                }

            }
            else
            {
                attackAudio.Stop();
                attackFlag = false;
                eyeLight.color = new Color(1, 0, 0, 1);
                effect.SetActive(false);
                ber.SetActive(false);
                attackTimer = 0;
            }
        }
    }
}
