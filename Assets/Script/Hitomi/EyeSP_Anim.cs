using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EyeSP_Anim : MonoBehaviour
{
    public GameObject eyeSP, kurome;
    private SpriteRenderer eyeSprite, kuromeSprite;
    private Collider2D eyeTrigger;
    private Animator anim;

    [HideInInspector] public bool appearEye = false;

    private bool appearFlag = false, animFlag = false;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        eyeSprite = eyeSP.GetComponent<SpriteRenderer>();
        eyeTrigger = eyeSP.GetComponent<Collider2D>();
        kuromeSprite = kurome.GetComponent<SpriteRenderer>();
        eyeSprite.enabled = false;
        eyeTrigger.enabled = false;
        kuromeSprite.enabled = false;
        anim = eyeSP.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        eyeSprite.color = new Color(1, 0.5f, 0.5f, 1);

        if (appearEye)
        {
            // オブジェクトを可視化し、当たり判定を有効にする
            eyeSprite.enabled = true;
            eyeTrigger.enabled = true;
            kuromeSprite.enabled = true;

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

                    // オブジェクトを不可視化し、当たり判定を無効にする
                    eyeSprite.enabled = false;
                    eyeTrigger.enabled = false;
                    kuromeSprite.enabled = false;

                    appearFlag = false;
                    animFlag = false;
                }
            }
            kurome.SetActive(false);
        }
    }
}

