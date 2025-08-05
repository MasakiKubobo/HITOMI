using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Eye_Move : MonoBehaviour
{
    public float speed = 1;
    public Light2D HpLight;
    public GameObject player, kurome, attention;
    private Light2D eyeLight;
    public GameObject effect;
    public GameObject ber;
    public float attackTime = 3;
    private float attackTimer;

    public AudioSource openAudio, attackAudio;
    private bool openFlag, attackFlag;

    [HideInInspector] public Vector2 kuromePos;
    [HideInInspector] public bool appear;
    bool follow = true, eyeAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        eyeLight = kurome.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Eye_Anim eye_Anim = GetComponent<Eye_Anim>();
        Collider2D collider = effect.GetComponent<Collider2D>();

        if (!eye_Anim.eyeAbility)
        {
            Vector3 followVec = player.transform.position - transform.position;
            Attentions attentions = attention.GetComponent<Attentions>();

            if (follow) // 主人公について来る
            {
                transform.position += followVec * speed * Time.deltaTime;
            }
            if (follow) // 追従中、瞳の黒目が主人公の方を向く
            {
                if (attentions.attention)
                {
                    kurome.transform.localPosition = attentions.kuromePos.normalized / 5;
                }
                else kurome.transform.localPosition = followVec.normalized / 5;
            }
            else
            {
                kurome.transform.localPosition = attentions.kuromePos.normalized / 5;
            }


            HpLight.intensity = 1;

            if (attackTimer > 0) // 回復するやんけ
            {
                attackTimer -= Time.deltaTime * 0.5f;
                ber.SetActive(true);
            }
            else
            {
                attackTimer = 0;
                ber.SetActive(false);
            }

            openFlag = false;

            collider.enabled = false;
            attackAudio.Stop();
        }
        else
        {
            if (Math.Sqrt(kuromePos.x * kuromePos.x + kuromePos.y * kuromePos.y) > 0.8) // スティックの傾きが最大で有効に
            {
                kurome.transform.position = (Vector2)transform.position + kuromePos * 0.2f;

                // 逆三角関数を用いてVector2から角度の数値を入力
                kurome.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(kuromePos.y, kuromePos.x) * Mathf.Rad2Deg - 90);
            }

            HpLight.intensity = 0.4f;

            if (!openFlag)
            {
                //openAudio.Play();
                eyeAttack = true;
                openFlag = true;
            }

            if (eyeAttack) // ゲージが尽きるまで攻撃が続く
            {
                collider.enabled = true;
                eyeLight.color = new Color(0, 0, 1, 1);
                effect.SetActive(true);
                ber.SetActive(true);

                if (!attackFlag)
                {
                    attackAudio.Play();
                    attackFlag = true;
                }

                attackTimer += Time.deltaTime * 0.7f;

                if (attackTimer >= attackTime) eyeAttack = false;

            }
            else
            {
                collider.enabled = false;
                attackAudio.Stop();
                attackFlag = false;
                eyeLight.color = new Color(1, 0, 0, 1);
                effect.SetActive(false);
                ber.SetActive(false);
            }
        }

        Slider slider = ber.GetComponent<Slider>();
        slider.value = (attackTime - attackTimer) / attackTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            follow = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            follow = true;
        }
    }
}
