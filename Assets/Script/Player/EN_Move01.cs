using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EN_Move01 : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private GameObject eye;
    bool chase = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eye = GameObject.Find("eye");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float xSpeed = 0;

        if (chase)
        {
            if (eye.transform.position.x > transform.position.x) xSpeed = moveSpeed;
            else xSpeed = -moveSpeed;
        }

        rb.velocity = new Vector2(xSpeed, 0);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        PrefabID prefabID = other.gameObject.GetComponent<PrefabID>();
        if (prefabID != null)
        {
            if (prefabID.ID == "attack_01")
            {
                Debug.Log("ヒット");
                Destroy(gameObject);
            }
        }
    }
}
