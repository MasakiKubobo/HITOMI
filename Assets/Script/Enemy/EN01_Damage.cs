using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN01_Damage : MonoBehaviour
{
    public int HP = 3;
    public float knockBackPowor = 100;

    [Space(5)]
    public ParticleSystem damagePar;
    public ParticleSystem destroyPar;

    private float invincibleTimer = 1;
    private Rigidbody2D rb;

    [HideInInspector] public bool knockBack = false; // ノックバック中か否か

    EN01_Anim en01_Anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        en01_Anim = GetComponent<EN01_Anim>();
    }

    // Update is called once per frame
    void Update()
    {
        // ノックバック後、0.2秒で無敵状態を解除
        if (knockBack)
        {
            en01_Anim.damage = true;
            invincibleTimer += Time.deltaTime;

            if (invincibleTimer > 0.2)
            {
                knockBack = false;
                en01_Anim.damage = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PrefabID prefabID = other.gameObject.GetComponent<PrefabID>();
        if (prefabID != null)
        {
            // 攻撃を受けた場合
            if (prefabID.ID == "attack_01")
            {
                Vector3 vector = transform.position;
                vector.y += 0.5f;
                KnockBack(other.transform.position, other.bounds.ClosestPoint(vector), knockBackPowor);
            }
        }
    }

    // ノックバック処理
    void KnockBack(Vector2 PLvec, Vector2 ConPos, float powor)
    {
        float moveDirection = transform.position.x - PLvec.x;
        if (moveDirection >= 0) moveDirection = 1;
        else moveDirection = -1;

        if (invincibleTimer > 0.2)
        {
            knockBack = true;

            rb.AddForce(Vector2.right * moveDirection * powor, ForceMode2D.Impulse);
            Instantiate(damagePar, ConPos, Quaternion.identity);

            HP--;
            if (HP <= 0) // HPが0になると撃破
            {
                Vector3 vector = transform.position;
                vector.y += 0.5f;
                Instantiate(destroyPar, vector, Quaternion.identity);
                Destroy(gameObject);
            }

            invincibleTimer = 0;
        }

    }
}
