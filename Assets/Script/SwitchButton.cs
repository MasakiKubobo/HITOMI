using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    private float down;
    private bool isDownning;
    [HideInInspector] public bool isSwitchOn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDownning)
        {
            down -= Time.deltaTime * 2f;
        }
        else
        {
            down += Time.deltaTime * 2f;
        }

        down = Mathf.Clamp01(down);
        transform.localPosition = new Vector2(0, down);

        if (down >= 1) isSwitchOn = false;
        if (down <= 0) isSwitchOn = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Item") || other.CompareTag("Enemy"))
        {
            isDownning = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Item") || other.CompareTag("Enemy"))
        {
            isDownning = false;
        }
    }
}
