using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EN_Move02 : MonoBehaviour
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
        Vector2 vec = Vector2.zero;

        if (chase)
        {
            vec = eye.transform.position - transform.position;
        }

        rb.velocity = vec.normalized * moveSpeed;
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
