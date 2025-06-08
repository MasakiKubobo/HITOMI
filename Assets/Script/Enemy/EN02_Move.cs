using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EN02_Move : MonoBehaviour
{
    public float moveSpeed;
    public int HP = 3;
    public float powor = 100;
    public ParticleSystem damagePar, destroyPar;

    private Rigidbody2D rb;
    private GameObject eye, eyeSP;
    private float timer = 0, timer2 = 0;
    public float knockBackTime = 1f;
    private bool knockBack = false;
    bool chase = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eye = GameObject.Find("eye");
        eyeSP = GameObject.Find("eyeSP");
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBack)
        {
            timer += Time.deltaTime;
            if (timer >= knockBackTime)
            {
                knockBack = false;
                timer = 0;
            }
        }

        if (HP <= 0)
        {
            Instantiate(destroyPar, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        timer2 += Time.deltaTime;
    }

    void FixedUpdate()
    {
        Vector2 vec = Vector2.zero;

        EyeSP_Move eyeSP_Move = eyeSP.GetComponent<EyeSP_Move>();

        if (chase)
        {
            vec = !eyeSP_Move.appear ? eye.transform.position - transform.position : eyeSP.transform.position - transform.position;
        }

        if (!knockBack) rb.velocity = vec.normalized * moveSpeed;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        PrefabID prefabID = other.gameObject.GetComponent<PrefabID>();
        if (prefabID != null)
        {
            if (prefabID.ID == "attack_01")
            {
                KnockBack(other.transform.position, other.bounds.ClosestPoint(transform.position), powor);
            }
        }
    }

    void KnockBack(Vector2 PLvec, Vector2 ConPos, float powor)
    {
        Vector2 vec = (Vector2)transform.position - PLvec;

        if (timer2 > 0.2)
        {
            rb.AddForce(vec.normalized * powor, ForceMode2D.Impulse);

            Instantiate(damagePar, ConPos, Quaternion.identity);
            HP--;
            if (!knockBack) knockBack = true;

            timer2 = 0;
        }
    }
}
