using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Materialize : MonoBehaviour
{
    public float MtrTime = 2;
    public ParticleSystem charge, mtr;
    public GameObject collision;

    public GameObject eyeSP;

    private float timer = 0;
    private bool Mtr = false;
    // Start is called before the first frame update
    void Start()
    {
        charge.Stop();
        mtr.Stop();
        collision.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Light2D light2D = GetComponent<Light2D>();
        EyeSP_Move eyeSP_Move = eyeSP.GetComponent<EyeSP_Move>();

        if (!eyeSP_Move.appear)
        {
            Mtr = false;
            timer = 0;
        }

        if (!Mtr)
        {
            collision.SetActive(false);
            light2D.intensity = 1;
        }
        else
        {
            charge.Stop();
            if (light2D.intensity == 1) mtr.Play();

            collision.SetActive(true);
            light2D.intensity = 3;
            Debug.Log("オラァ");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("eye"))
        {
            charge.Play();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("eye"))
        {
            if (!Mtr)
            {
                if (timer >= MtrTime)
                {
                    Mtr = true;
                }
                timer += Time.deltaTime;
            }

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("eye"))
        {
            charge.Stop();
            timer = 0;
        }
    }
}
