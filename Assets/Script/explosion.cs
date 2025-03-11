using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public float explosionTime = 0.5f;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= explosionTime)
        {
            Destroy(gameObject);
        }

        timer += Time.deltaTime;
    }
}
