using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitomi_follow : MonoBehaviour
{
    public GameObject player;
    public float speed = 1;

    bool follow = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            Vector3 vec = player.transform.position - transform.position;
            transform.position += new Vector3(vec.x * speed * Time.deltaTime, vec.y * speed * Time.deltaTime, 0);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            follow = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            follow = true;
        }
    }
}
