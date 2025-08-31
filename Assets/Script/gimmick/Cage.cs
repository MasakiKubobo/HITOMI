using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public bool isBox = false;
    public Rigidbody2D rb;
    private GameObject eye;
    public float powor;
    public AudioSource ironAudio;
    public GameObject effect;

    private int hp = 10;
    private float damageTimer;
    private bool damageFlag;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        eye = GameObject.Find("eye");
        rb.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        Materialize materialize = GetComponent<Materialize>();

        if (materialize.Mtr)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            if (!isBox)
            {
                //eye.transform.SetParent(transform);
                eye.transform.position = transform.position;
                eye.transform.rotation = transform.rotation;

                Collider2D collider2D = eye.GetComponent<Collider2D>();
                collider2D.enabled = false;
            }
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
            if (!isBox)
            {
                //eye.transform.SetParent(null);
                eye.transform.eulerAngles = Vector3.zero;
                Collider2D collider2D = eye.GetComponent<Collider2D>();
                collider2D.enabled = true;
            }
        }

        if (damageFlag)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 0.5)
            {
                damageFlag = false;
                damageTimer = 0;
            }
        }

        if (!isBox)
        {
            PL_Move pL_Move = player.GetComponent<PL_Move>();

            if (hp <= 2)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.red;
            }
            if (hp <= 0)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
                //eye.transform.SetParent(null);
                eye.transform.eulerAngles = Vector3.zero;
                Collider2D collider2D = eye.GetComponent<Collider2D>();
                collider2D.enabled = true;
                pL_Move.isHanding = false;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isBox && !damageFlag)
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                Vector2 powerVec = other.transform.position - transform.position;
                rb.AddForce(powerVec * 10, ForceMode2D.Impulse);
                hp--;
                damageFlag = true;
                ironAudio.Play();
            }
        }

        PrefabID prefabID = other.gameObject.GetComponent<PrefabID>();
        if (prefabID != null)
        {
            if (prefabID.ID == "attack_01")
            {
                Materialize materialize = GetComponent<Materialize>();
                if (materialize.Mtr)
                {
                    if (!damageFlag)
                    {
                        KnockBack(other.transform.position, other.bounds.ClosestPoint(transform.position), powor);
                        damageFlag = true;
                        ironAudio.Play();
                    }
                }
            }
        }
    }

    void KnockBack(Vector2 PLvec, Vector2 ConPos, float powor)
    {
        Vector2 vec = (Vector2)transform.position - PLvec;
        rb.AddForce(vec.normalized * powor, ForceMode2D.Impulse);
        hp -= 2;
    }
}
