using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN_Manager : MonoBehaviour
{
    public GameObject player;
    public GameObject minBox, maxBox;
    public GameObject[] enemys;
    public float interval = 1;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        if (timer >= interval)
        {
            float minX = minBox.transform.position.x;
            float maxX = maxBox.transform.position.x;
            float minY = minBox.transform.position.y;
            float maxY = maxBox.transform.position.y;

            Instantiate(enemys[(int)Random.Range(0, 2)], new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity);

            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
