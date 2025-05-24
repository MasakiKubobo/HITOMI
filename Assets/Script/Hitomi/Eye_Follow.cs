using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Follow : MonoBehaviour
{
    public GameObject player;
    public float speed = 1;

    public GameObject kurome;

    bool follow = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followVec = player.transform.position - transform.position;

        if (follow) // 主人公について来る
        {
            transform.position += followVec * speed * Time.deltaTime;
        }

        if (follow) // 追従中、瞳の黒目が主人公の方を向く
        {
            kurome.transform.localPosition = followVec.normalized / 5;
        }


    }

    void FixedUpdate()
    {

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
