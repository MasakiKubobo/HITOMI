using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_throw : MonoBehaviour
{
    public GameObject knife;
    public float interval = 1;

    private bool _throw = false;
    private float timer = 0;
    public GameObject hitomi;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (interval <= timer)
        {
            Instantiate(knife, transform.position, Quaternion.identity);
            timer = 0;
        }

        if (_throw) timer += Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("eye"))
        {
            hitomi_move hitomi_Move = hitomi.GetComponent<hitomi_move>();
            if (hitomi_Move.Active) _throw = true;
            else _throw = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("eye"))
        {
            _throw = false;
        }
    }
}
