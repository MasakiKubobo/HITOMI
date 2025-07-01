using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Anim : MonoBehaviour
{
    public GameObject eye, kurome, player;
    private SpriteRenderer eyeSprite, kuromeSprite;
    private Collider2D eyeTrigger;
    private Animator anim;

    [HideInInspector] public bool appearEye = true;

    private bool appearFlag = true, animFlag = false;

    public bool tutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        eyeSprite = eye.GetComponent<SpriteRenderer>();
        eyeTrigger = eye.GetComponent<Collider2D>();
        kuromeSprite = kurome.GetComponent<SpriteRenderer>();
        anim = eye.GetComponent<Animator>();

        if (GameManager.pointer >= 2) tutorial = false;
        if (tutorial)
        {
            appearEye = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (tutorial)
        {
            float distance = Mathf.Abs(eye.transform.position.x - player.transform.position.x);

            if (distance <= 10)
            {
                appearEye = true;
                tutorial = false;
            }
        }


        if (!tutorial)
        {


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
                        animFlag = true;
                    }
                    if (animFlag && anim.GetCurrentAnimatorStateInfo(0).IsName("New State"))
                    {
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
}
