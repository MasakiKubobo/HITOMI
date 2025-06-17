using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Materialize : MonoBehaviour
{
    public float mtrTime = 2;
    public ParticleSystem charge, mtr, smoke;
    public Collider2D collision;

    private GameObject eyeSP;

    private float mtrTimer = 0;
    [HideInInspector] public bool Mtr = false;  // 実体化しているか
    private bool mtring = false; // 実体化の際中か
    private bool smokeFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        eyeSP = GameObject.Find("eyeSP");

        charge.Stop();
        mtr.Stop();
        smoke.Stop();
        collision.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Light2D light2D = GetComponent<Light2D>();
        EyeSP_Move eyeSP_Move = eyeSP.GetComponent<EyeSP_Move>();

        // 瞳の能力解除
        if (!eyeSP_Move.appear)
        {
            Mtr = false;
            mtring = false;
        }

        if (mtring) // 実体化の際中
        {
            if (mtrTimer >= mtrTime)
            {
                Mtr = true;
                mtring = false;
            }
            mtrTimer += Time.deltaTime;
        }
        else
        {
            mtrTimer = 0;
            charge.Stop();
        }

        if (!Mtr)
        {
            if (!smokeFlag)
            {
                smoke.Play();
                smokeFlag = true;
            }
            collision.enabled = false;
            light2D.intensity = 1;
        }
        else
        {
            smokeFlag = false;
            charge.Stop();
            if (light2D.intensity == 1) mtr.Play();

            collision.enabled = true;
            light2D.intensity = 3;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("vision"))
        {
            mtring = true;
            charge.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("vision"))
        {
            mtring = false;
        }
    }
}
