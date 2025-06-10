using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EyeSP_Damage : MonoBehaviour
{
    public GameObject kurome, eye;
    private SpriteRenderer kuromeSprite;

    public float[] damages;
    public float[] attackDamages;
    [HideInInspector] public bool invincible = false;
    public float invincibleTime = 0.5f;

    // フラッシング関連の変数
    private float flashTimer = 0;
    private bool flashFlag = false;
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kuromeSprite = kurome.GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            kuromeSprite.color = Color.clear; // 黒目を見えなくする

            // ライトと能力を非活性化する
            Light2D light2D = kurome.GetComponent<Light2D>();
            Collider2D collider2D = kurome.GetComponent<Collider2D>();
            light2D.enabled = false;
            collider2D.enabled = false;

            timer += Time.deltaTime;
            if (timer >= invincibleTime)
            {
                kuromeSprite.color = Color.black; // 黒目を見えるようにする

                // ライトと能力を活性化する
                light2D.enabled = true;
                collider2D.enabled = true;

                invincible = false;
                timer = 0;
            }
        }
        DamageFlash();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PrefabID prefabID = other.GetComponent<PrefabID>();
        if (prefabID == null) return;

        switch (prefabID.ID)
        {
            case "enemy_01": // enemy_01にぶつかった場合
                Damage(damages[0]);
                break;
            case "enemy_02": // enemy_02にぶつかった場合
                Damage(damages[1]);
                break;
            case "enemy_03": // enemy_03にぶつかった場合
                Damage(damages[2]);
                break;
            case "attack_EN01": // attack_EN01に当たった場合
                Damage(attackDamages[0]);
                break;
            case "attack_EN02": // attack_EN02に当たった場合
                Damage(attackDamages[1]);
                break;
        }
    }

    void Damage(float damage)
    {
        if (!invincible)
        {
            Eye_HP.HP -= damage;
            invincible = true;
        }
    }

    void DamageFlash()
    {
        Color color = spriteRenderer.color;
        if (invincible)
        {
            flashTimer += Time.deltaTime;
            if (flashTimer >= 0.1)
            {
                if (!flashFlag)
                {
                    spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, color.a);
                    flashFlag = true;
                }
                else
                {
                    spriteRenderer.color = new Color(1, 1, 1, color.a);
                    flashFlag = false;
                }
                flashTimer = 0;
            }
        }
        else spriteRenderer.color = new Color(1, 1, 1, color.a);
    }
}
