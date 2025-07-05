using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Follow : MonoBehaviour
{
    public GameObject player, attention;
    public float speed = 1;

    public GameObject kurome, eyeSP;

    bool follow = true;

    public bool tutorial = false;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.pointer >= 2) tutorial = false;
        if (tutorial) follow = false;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 followVec = player.transform.position - transform.position;

        Attentions attentions = attention.GetComponent<Attentions>();

        if (follow) // 主人公について来る
        {
            transform.position += followVec * speed * Time.deltaTime;
        }

        if (follow) // 追従中、瞳の黒目が主人公の方を向く
        {
            Debug.Log(attentions.attention);
            if (attentions.attention)
            {
                kurome.transform.localPosition = attentions.kuromePos.normalized / 5;
            }
            else kurome.transform.localPosition = followVec.normalized / 5;
        }
        else
        {
            kurome.transform.localPosition = attentions.kuromePos.normalized / 5;
        }

        EyeSP_Move eyeSP_Move = eyeSP.GetComponent<EyeSP_Move>();
        if (eyeSP_Move.appear)
        {
            transform.position = player.transform.position;
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
