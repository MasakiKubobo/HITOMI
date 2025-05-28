using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EN_Move01 : MonoBehaviour
{
    public float moveSpeed;
    public int HP = 3;
    public float powor = 100;

    private Rigidbody2D rb;
    private GameObject eye;
    private float timer = 0;
    private float invTime = 1f;
    private bool invincible = false;
    private Vector2 knock;
    bool chase = true, inAir = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eye = GameObject.Find("eye");
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            timer += Time.deltaTime;
            if (timer >= invTime)
            {
                invincible = false;
                timer = 0;
            }
        }

        if (HP <= 0) Destroy(gameObject);
    }

    void FixedUpdate()
    {
        Vector2 vec = Vector2.zero;

        if (chase)
        {
            vec = eye.transform.position - transform.position;
        }

        if (!invincible)
        {
            if (!inAir) rb.velocity = new Vector2(vec.normalized.x * moveSpeed, 0);
            else rb.velocity = new Vector2(0, 0);
        }
        else rb.velocity = knock;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        PrefabID prefabID = other.gameObject.GetComponent<PrefabID>();
        if (prefabID != null)
        {
            if (prefabID.ID == "attack_01")
            {
                KnockBack(other.transform.position, powor);
            }
        }

        if (other.gameObject.CompareTag("Ground")) inAir = false;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) inAir = true;
    }

    void KnockBack(Vector2 PLvec, float powor)
    {
        Vector2 vec = (Vector2)transform.position - PLvec;

        if (!invincible)
        {
            knock = vec.normalized * powor;

            Debug.Log(vec.normalized * powor);
            HP--;
            invincible = true;
        }
    }
}
