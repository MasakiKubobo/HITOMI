using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN04_Attack : MonoBehaviour
{
    public bool attack = false;
    public GameObject bullet;

    private bool attackFlag = false;
    private Animator anim;
    private float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (attackTimer <= 0.3)
        {
            anim.SetBool("enAttack", true);
            attackTimer += Time.deltaTime;
        }
        else
        {
            anim.SetBool("enAttack", false);
        }

        if (attack)
        {
            if (!attackFlag)
            {
                //Instantiate(bullet, );
                attackFlag = true;
            }
        }
        else attackFlag = false;

    }
}
