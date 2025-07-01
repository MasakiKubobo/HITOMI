using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attentions : MonoBehaviour
{
    public GameObject eye;
    public Eye_Anim eyeManager;

    [HideInInspector] public Vector2 kuromePos;
    public List<GameObject> objects = new List<GameObject>();

    private SpriteRenderer spriteRenderer;
    private float attentionTimer = 10;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (eyeManager.appearEye)
        {
            if (objects.Count != 0)
            {
                Vector2 objPos = objects[objects.Count - 1].transform.position;
                kuromePos = objPos - (Vector2)eye.transform.position;

                attentionTimer += Time.deltaTime;
            }
            else
            {
                kuromePos = Vector2.zero;
            }

            if (attentionTimer <= 1) spriteRenderer.enabled = true;
            else spriteRenderer.enabled = false;
        }
        else spriteRenderer.enabled = false;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        objects.Add(other.gameObject);
        attentionTimer = 0;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        objects.Remove(other.gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {

    }
}
