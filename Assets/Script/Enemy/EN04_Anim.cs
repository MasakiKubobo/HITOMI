using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN04_Attack : MonoBehaviour
{
    public bool attack = false;
    public GameObject bullet;
    public float attackTime;
    public float upPower = 10;

    private bool attackFlag, recoilFlag;
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
        EN04_Move eN04_Move = GetComponent<EN04_Move>();
        EN04_Damage eN04_Damage = GetComponent<EN04_Damage>();

        if (attackTimer >= attackTime)
        {
            if (eN04_Move.attackMode) anim.SetTrigger("enAttack");
            attackTimer = 0;
        }
        attackTimer += Time.deltaTime;

        if (eN04_Damage.damageAnim)
        {
            anim.Play("EN5_Damage");
            eN04_Damage.damageAnim = false;
        }


        if (attack)
        {
            if (!attackFlag)
            {
                GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity);
                Rigidbody2D BulletRb = Bullet.GetComponent<Rigidbody2D>();
                BulletRb.AddForce(Vector2.up * upPower, ForceMode2D.Impulse);
                attackFlag = true;
            }
        }
        else attackFlag = false;

    }
}
