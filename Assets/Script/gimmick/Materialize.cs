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

    private AudioSource chargeAudio, mtrAudio, smokeAudio;
    // Start is called before the first frame update
    void Start()
    {
        eyeSP = GameObject.Find("eyeSP");

        charge.Stop();
        mtr.Stop();
        smoke.Stop();
        collision.enabled = false;

        chargeAudio = charge.GetComponent<AudioSource>();
        mtrAudio = mtr.GetComponent<AudioSource>();
        smokeAudio = smoke.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Light2D light2D = GetComponent<Light2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color srColor = sr.color;
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
                smokeAudio.Play();
                smokeFlag = true;
            }
            collision.enabled = false;
            light2D.intensity = 1f;

            srColor.a = 0.3f;
            sr.color = srColor;
        }
        else
        {
            smokeFlag = false;
            charge.Stop();
            if (light2D.intensity == 1) { mtr.Play(); mtrAudio.Play(); }

            collision.enabled = true;
            light2D.intensity = 3;

            srColor.a = 1;
            sr.color = srColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("vision"))
        {
            mtring = true;
            if (!Mtr)
            {
                charge.Play();
                chargeAudio.Play();
            }
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
