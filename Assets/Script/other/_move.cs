using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _move : MonoBehaviour
{
    public GameObject animator;
    private Animator anim;
    private bool onFloor = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            animator.transform.localScale = new Vector3(-0.83f, 0.83f, 0.5f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.transform.localScale = new Vector3(0.83f, 0.83f, 0.5f);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("idle", false);
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("idle", true);
        }
    }
}
