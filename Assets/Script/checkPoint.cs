using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    public GameObject effect;
    private SpriteRenderer sprite;
    [HideInInspector] public bool checkFlag;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!checkFlag)
            {
                sprite.color = new Color(1, 1, 1, 1);
                Instantiate(effect, transform.position, Quaternion.identity);
                checkFlag = true;
            }
        }
    }
}
