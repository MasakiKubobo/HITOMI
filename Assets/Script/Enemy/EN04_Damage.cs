using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN04_Damage : MonoBehaviour
{
    public int HP = 3;

    [Space(5)]
    public ParticleSystem damagePar;
    public ParticleSystem destroyPar;

    private float invincibleTimer = 1;
    private Rigidbody2D rb;

    [HideInInspector] public bool knockBack = false; // ノックバック中か否か
    [HideInInspector] public bool damageAnim = false;

    public bool isEnemy05 = false;
    public bool weakIsEye = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ノックバック後、0.2秒で無敵状態を解除
        if (knockBack)
        {
            invincibleTimer += Time.deltaTime;

            if (invincibleTimer > 0.2)
            {
                knockBack = false;
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
                if (!isEnemy05)
                {
                    Vector3 vector = transform.position;
                    vector.y += 0.5f;
                    KnockBack(other.transform.position, other.bounds.ClosestPoint(vector));
                }
                else damageAnim = true;
            }
            else if (prefabID.ID == "attack_02")
            {
                if (!isEnemy05 || weakIsEye)
                {
                    Vector3 vector = transform.position;
                    vector.y += 0.5f;
                    KnockBack(other.transform.position, other.bounds.ClosestPoint(vector));
                }
                else damageAnim = true;
            }
            else if (prefabID.ID == "attack_03")
            {
                Materialize materialize = other.GetComponent<Materialize>();
                if (materialize.Mtr)
                {
                    Vector3 vector = transform.position;
                    vector.y += 0.5f;
                    KnockBack(other.transform.position, other.bounds.ClosestPoint(vector));
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PrefabID prefabID = other.gameObject.GetComponent<PrefabID>();
        if (prefabID != null)
        {
            if (prefabID.ID == "attack_03")
            {
                Materialize materialize = other.GetComponent<Materialize>();
                if (materialize.Mtr)
                {
                    Vector3 vector = transform.position;
                    vector.y += 0.5f;
                    KnockBack(other.transform.position, other.bounds.ClosestPoint(vector));
                }
            }
        }
    }

    // ノックバック処理
    void KnockBack(Vector2 PLvec, Vector2 ConPos)
    {

        if (invincibleTimer > 0.2)
        {
            knockBack = true;
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
