using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Damage : MonoBehaviour
{
    public float damageTime = 0.3f;
    public float invincibleTime = 0.6f;
    public ParticleSystem damagePar;

    private float timer = 0;
    private bool invincible = false;
    private Vector3 damagePos;

    // フラッシング関連の変数
    private float flashTimer = 0;
    private bool flashFlag = false;
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public bool damage = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!damage)
        {
            float X = transform.eulerAngles.x;
            float Y = transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(X, Y, 0);
            damagePos = transform.position;
        }
        else
        {
            transform.position = damagePos;
            transform.Rotate(0, 0, 360 / damageTime * Time.deltaTime, Space.World);
        }


        if (invincible) timer += Time.deltaTime;
        DamageFlash();

        if (timer >= damageTime)
        {
            damage = false;
        }
        if (timer >= invincibleTime)
        {
            invincible = false;
            timer = 0;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        PrefabID prefabID = other.gameObject.GetComponent<PrefabID>();
        if (prefabID == null) return;

        switch (prefabID.ID)
        {
            case "enemy_01":
                KnockBack(other.transform.position, 0);
                break;
            case "enemy_02":
                KnockBack(other.transform.position, 0);
                break;
        }
    }

    void KnockBack(Vector2 ENvec, float powor)
    {
        //Vector2 vec = (Vector2)eye.transform.position - ENvec;

        if (!invincible)
        {
            Instantiate(damagePar, transform.position, Quaternion.identity);
            //rb.AddForce(vec.normalized * powor, ForceMode2D.Impulse);
            damage = true;
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
