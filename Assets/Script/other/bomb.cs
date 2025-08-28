using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public GameObject bombObject;
    public GameObject explosionObject;
    public float bombTime;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AppearObject appearObject = bombObject.GetComponent<AppearObject>();

        if (appearObject.Materialized)
        {
            timer += Time.deltaTime;
            if (timer >= bombTime)
            {
                Instantiate(explosionObject, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
