using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    public float speed = 1;
    private GameObject HITOMI;
    private bool flag = false;
    private Vector2 direction;

    void Awake()
    {
        HITOMI = GameObject.Find("Hitomi");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!flag)
        {
            direction = (HITOMI.transform.position - transform.position).normalized;
            flag = true;
        }

        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            if (other.gameObject.GetComponent<AppearObject>() != null)
            {
                AppearObject appearObject = other.gameObject.GetComponent<AppearObject>();
                if (appearObject.Materialized) Destroy(gameObject);
            }

        }

        if (other.gameObject == HITOMI) Destroy(gameObject);
    }
}
