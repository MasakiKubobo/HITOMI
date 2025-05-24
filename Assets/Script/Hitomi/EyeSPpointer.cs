using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSPpointer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color defaultColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);
    private Color redColor = new Color(1, 0, 0, 0.5f);

    [HideInInspector] public bool canSummon = true;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            canSummon = false;
            spriteRenderer.color = redColor;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            canSummon = true;
            spriteRenderer.color = defaultColor;
        }
    }

}
