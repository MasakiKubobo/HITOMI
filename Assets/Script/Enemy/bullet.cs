using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    GameObject eye;
    public float speed;
    public GameObject Effect;
    private float lifeTimer;

    private Rigidbody2D rb;
    private Vector2 vector2;
    // Start is called before the first frame update
    void Start()
    {
        eye = GameObject.Find("eye");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTimer == 0)
        {
            vector2 = eye.transform.position - transform.position;
        }
        rb.AddForce(vector2 * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(Effect, other.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        PrefabID prefabID = other.GetComponent<PrefabID>();
        if (prefabID == null) return;

        switch (prefabID.ID)
        {
            case "attack_01": // 攻撃エフェクトにぶつかった場合
                Instantiate(Effect, other.transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
        }
    }
}
