using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PL_Attack : MonoBehaviour
{
    [HideInInspector] public bool attack;
    public float attackLimit = 0.2f;
    public float coolTime = 0.3f;
    public GameObject effect;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {

            timer += Time.deltaTime;
            if (timer < attackLimit)
            {
                effect.SetActive(true);
            }
            else
            {
                effect.SetActive(false);

                if (timer >= attackLimit + coolTime) attack = false;
            }
        }
        else
        {
            effect.SetActive(false);
            timer = 0;
        }

    }
}
