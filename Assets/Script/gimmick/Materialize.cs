using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Materialize : MonoBehaviour
{
    public bool repeat = true; // 能力解除で元に戻るか否か
    public float mtrTime = 2;
    public ParticleSystem charge, mtr, smoke;
    public Collider2D collision;

    private GameObject eye;

    private float mtrTimer = 0;
    [HideInInspector] public bool Mtr = false;  // 実体化しているか
    private bool mtring = false; // 実体化の際中か
    private bool smokeFlag = true;

    private AudioSource chargeAudio, mtrAudio, smokeAudio;
    private Rigidbody2D rb;

    public SpriteRenderer sr, addSprite;
    public GameObject hideObject;
    public bool dynamic;
    // Start is called before the first frame update
    void Start()
    {
        eye = GameObject.Find("eye");

        charge.Stop();
        mtr.Stop();
        smoke.Stop();
        collision.enabled = false;

        chargeAudio = charge.GetComponent<AudioSource>();
        mtrAudio = mtr.GetComponent<AudioSource>();
        smokeAudio = smoke.GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Light2D light2D = GetComponent<Light2D>();
        Color srColor = sr.color;
        Eye_Anim eye_Anim = eye.GetComponent<Eye_Anim>();

        // 瞳の能力解除
        if (!eye_Anim.eyeAbility)
        {
            if (repeat) Mtr = false; mtring = false;
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
            rb.isKinematic = true;
            if (hideObject != null) hideObject.SetActive(false);

            if (!smokeFlag)
            {
                smoke.Play();
                smokeAudio.Play();
                smokeFlag = true;
            }
            collision.enabled = false;
            light2D.intensity = 1f;

            srColor = Color.black;
            srColor.a = Mathf.PingPong(Time.time * 0.5f, 0.5f);
            sr.color = srColor;
            if (addSprite != null) addSprite.color = srColor;
        }
        else
        {
            if (hideObject != null) hideObject.SetActive(true);
            if (dynamic) rb.isKinematic = false;

            smokeFlag = false;
            charge.Stop();
            if (light2D.intensity == 1) { mtr.Play(); mtrAudio.Play(); }

            collision.enabled = true;
            light2D.intensity = 3;

            srColor = Color.white;
            srColor.a = 1;
            sr.color = srColor;
            if (addSprite != null) addSprite.color = srColor;
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
