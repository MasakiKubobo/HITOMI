using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Motion : MonoBehaviour
{
    public Sprite normalSP, attackSP, damageSP;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PL_Attack pL_Attack = GetComponent<PL_Attack>();
        PL_Damage pL_Damage = GetComponent<PL_Damage>();

        if (pL_Damage.damage)
        {
            spriteRenderer.sprite = damageSP;
            spriteRenderer.flipX = true;
            spriteRenderer.size = new Vector2(1.5f, 3f);
        }
        else
        {
            if (pL_Attack.attack)
            {
                spriteRenderer.sprite = attackSP;
                spriteRenderer.flipX = false;
                spriteRenderer.size = new Vector2(2f, 3f);
            }
            else
            {
                spriteRenderer.sprite = normalSP;
                spriteRenderer.flipX = true;
                spriteRenderer.size = new Vector2(1.5f, 2.5f);
            }
        }
    }
}
