using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

public class PL_Attack : MonoBehaviour
{
    [HideInInspector] public bool attack, attackFront, attackUp;
    public float coolTime = 0.3f;
    private float timer = 0;
    private bool attackFlag;

    public GameObject effect;
    public GameObject slush;

    private PL_Motion pL_Motion;
    private PL_Move pL_Move;
    // Start is called before the first frame update
    void Start()
    {
        pL_Motion = GetComponent<PL_Motion>();
        pL_Move = GetComponent<PL_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack || attackFront || attackUp)
        {
            if (!pL_Move.isHanding)
            {
                timer += Time.deltaTime;
                if (timer >= coolTime) { attack = false; attackFront = false; attackUp = false; }

                if (!attackFlag)
                {
                    Instantiate(effect, slush.transform.position, Quaternion.identity);
                    if (attack) pL_Motion.attack = true;
                    if (attackFront) pL_Motion.attackFront = true;
                    if (attackUp) pL_Motion.attackUp = true;
                    attackFlag = true;
                }
            }
            else
            {
                timer = 0;
                attackFlag = false;
                attack = false;
            }

        }
        else
        {
            timer = 0;
            attackFlag = false;
        }

    }
}
