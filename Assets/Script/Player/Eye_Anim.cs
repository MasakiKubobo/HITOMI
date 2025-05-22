using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Anim : MonoBehaviour
{
    public GameObject kurome;
    public Animator anim;

    [HideInInspector] public bool appearEye = true;

    private bool appearFlag = false;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (appearEye)
        {
            if (!appearFlag)
            {
                anim.SetBool("eyeClose", false);
                anim.SetBool("eyeOpen", true);
                appearFlag = true;
            }

            if (timer >= 0.3) //kurome.SetActive(true);
                timer += Time.deltaTime;
        }
        else
        {
            if (appearFlag)
            {
                Debug.Log("eye");
                anim.SetBool("eyeOpen", false);
                anim.SetBool("eyeClose", true);
                appearFlag = false;
            }

            //kurome.SetActive(false);
            timer = 0;
        }
    }
}
