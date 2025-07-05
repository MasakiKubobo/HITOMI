using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PL_Motion : MonoBehaviour
{
    public bool srAppear;
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public bool dash, jumpUp, jumpDown, damage, attack, attackFront, attackUp;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = srAppear;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PL_Attack pL_Attack = GetComponent<PL_Attack>();
        PL_Damage pL_Damage = GetComponent<PL_Damage>();

        anim.SetBool("Run", dash);
        anim.SetBool("JumpUp", jumpUp);
        anim.SetBool("JumpDown", jumpDown);
        anim.SetBool("Damage", damage);

        if (attack)
        {
            anim.Play("PL_Attack");
            attack = false;
        }

        if (attackFront)
        {
            anim.Play("PL_AttackFront");
            attackFront = false;
        }

        if (attackUp)
        {
            anim.Play("PL_AttackUp");
            attackUp = false;
        }

        spriteRenderer.enabled = srAppear;



    }
}
