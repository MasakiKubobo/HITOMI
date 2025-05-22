using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN_Manager : MonoBehaviour
{
    public GameObject enemy01, enemy02;
    bool EN1 = true;
    public float interval = 1;

    public float maxX, minX, maxY, minY;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= interval)
        {
            if (EN1)
            {
                Instantiate(enemy01, new Vector2(Random.Range(minX, maxX), -3), Quaternion.identity);
            }
            else
            {
                Instantiate(enemy02, new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity);
                EN1 = true;
            }

            if (EN1) EN1 = false;

            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
