using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN01_Anim : MonoBehaviour
{
    private Animator anim;

    [HideInInspector]
    public bool move = false, attack = false, damage = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetBool("enDamage", damage);

        if (!damage)
        {
            anim.SetBool("enAttack", attack);

            if (attack) attack = false;
            else anim.SetBool("enMove", move);
        }

    }
}
