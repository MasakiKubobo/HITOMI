using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Anim : MonoBehaviour
{
    public GameObject player;
    private Animator anim;

    [HideInInspector] public bool eyeAbility = false;

    private bool animFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        eyeAbility = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (eyeAbility)
        {
            // 能力開眼のアニメーションへ
            if (!animFlag)
            {
                anim.SetTrigger("eyeAbility");
                animFlag = true;
            }
        }
        else
        {
            // 能力解除のアニメーションへ
            if (animFlag)
            {
                anim.SetTrigger("eyeClose");
                animFlag = false;
            }
        }


    }

}

