using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Materialize : MonoBehaviour
{
    public bool repeat = true; // 能力解除で元に戻るか否か
    public float mtrTime = 2;
    public Sprite beforeSp, afterSp;
    public ParticleSystem charge, mtr;
    public Collider2D collision;

    private GameObject eye;

    private float mtrTimer = 0;
    [HideInInspector] public bool Mtr = false;  // 実体化しているか
    private bool mtring = false; // 実体化の際中か

    private AudioSource chargeAudio, mtrAudio;
    private Rigidbody2D rb;

    public SpriteRenderer sr, addSprite;
    public GameObject hideObject;
    public bool dynamic;

    private bool mtrFlag;
    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        eye = GameObject.Find("eye");

        charge.Stop();
        mtr.Stop();
        collision.enabled = false;

        chargeAudio = charge.GetComponent<AudioSource>();
        mtrAudio = mtr.GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Color srColor = sr.color;

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
            sr.sprite = beforeSp;

            rb.isKinematic = true;
            if (hideObject != null) hideObject.SetActive(false);

            mtrFlag = false;

            collision.enabled = false;

            srColor.a = Mathf.PingPong(Time.time * 2f, 1f);
            sr.color = srColor;
            if (addSprite != null) addSprite.color = srColor;
        }
        else
        {
            sr.sprite = afterSp;

            if (hideObject != null) hideObject.SetActive(true);
            if (dynamic) rb.isKinematic = false;

            charge.Stop();

            if (!mtrFlag)
            {
                mtr.Play();
                mtrAudio.Play();
                mtrFlag = true;
            }

            collision.enabled = true;

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
