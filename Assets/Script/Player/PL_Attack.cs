using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

public class PL_Attack : MonoBehaviour
{
    [HideInInspector] public bool attack;
    public float attackLimit = 0.2f;
    public float coolTime = 0.3f;
    private float timer = 0;
    private bool attackFlag;

    public GameObject effect;
    public GameObject slush;

    private PL_Motion pL_Motion;
    // Start is called before the first frame update
    void Start()
    {
        pL_Motion = GetComponent<PL_Motion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {

            timer += Time.deltaTime;
            if (timer < attackLimit)
            {
            }
            else
            {

                if (timer >= attackLimit + coolTime) attack = false;
            }

            if (!attackFlag)
            {
                Instantiate(effect, slush.transform.position, Quaternion.identity);
                pL_Motion.attack = true;
                attackFlag = true;
            }
        }
        else
        {
            timer = 0;
            attackFlag = false;
        }

    }
}
