using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EyeSP_Anim : MonoBehaviour
{
    public GameObject eyeSP, kurome;
    private SpriteRenderer eyeSprite;
    private Animator anim;

    [HideInInspector] public bool appearEye = false;

    private bool appearFlag = false, animFlag = false;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        eyeSprite = eyeSP.GetComponent<SpriteRenderer>();
        eyeSprite.color = Color.clear;
        anim = eyeSP.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (appearEye)
        {
            eyeSprite.color = Color.white;

            if (!appearFlag)
            {
                anim.SetBool("eyeOpen", true);

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("eye_open"))
                {
                    Time.timeScale = 0.3f;
                    animFlag = true;
                }
                if (animFlag && anim.GetCurrentAnimatorStateInfo(0).IsName("New State"))
                {
                    Time.timeScale = 1;
                    anim.SetBool("eyeOpen", false);
                    kurome.SetActive(true);

                    appearFlag = true;
                    animFlag = false;
                }
            }
        }
        else
        {
            if (appearFlag)
            {
                anim.SetBool("eyeClose", true);

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("eye_close"))
                {
                    animFlag = true;
                }
                if (animFlag && anim.GetCurrentAnimatorStateInfo(0).IsName("New State"))
                {
                    anim.SetBool("eyeClose", false);
                    eyeSprite.color = Color.clear;

                    appearFlag = false;
                    animFlag = false;
                }
            }
            kurome.SetActive(false);
        }
    }
}

