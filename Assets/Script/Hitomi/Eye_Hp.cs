using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eye_Hp : MonoBehaviour
{
    public GameObject kurome;
    private SpriteRenderer kuromeSprite;

    public Slider HPber;
    public GameObject HpLight;
    public float HP = 100;

    public float[] damages;
    public float[] knockBackPowor;
    public float invincibleTime = 0.5f;

    private Rigidbody2D rb;
    private bool invincible = false;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kuromeSprite = kurome.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            kuromeSprite.color = Color.clear; // 黒目を見えなくする

            timer += Time.deltaTime;
            if (timer >= invincibleTime)
            {
                kuromeSprite.color = Color.black; // 黒目を見えるようにする
                invincible = false;
                timer = 0;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PrefabID prefabID = other.GetComponent<PrefabID>();
        if (prefabID == null) return;

        switch (prefabID.ID)
        {
            case "enemy_01": // enemy_01にぶつかった場合
                KnockBack(other.transform.position, knockBackPowor[0], damages[0]);
                break;
            case "enemy_02": // enemy_02にぶつかった場合
                KnockBack(other.transform.position, knockBackPowor[1], damages[1]);
                break;
        }
    }

    void KnockBack(Vector2 ENvec, float powor, float damage)
    {
        Vector2 vec = (Vector2)transform.position - ENvec;

        if (!invincible)
        {
            HP -= damage;
            HPber.value = HP / 100;
            rb.AddForce(vec.normalized * powor, ForceMode2D.Impulse);
            invincible = true;
        }
    }
}
