using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Gate : MonoBehaviour
{
    public GameObject vCam;
    public ParticleSystem charge, open;
    private GameObject eyeSP;

    public float openSpeed, closeSpeed;
    public float openMax;
    private Vector3 openPos, closePos;

    private bool bathe = false;
    private bool endOpen = false, shake = false, shakeFlag = false;
    private float moveTimer = 0;

    public AudioSource gogogoAudio, gateAudio;
    // Start is called before the first frame update
    void Start()
    {
        eyeSP = GameObject.Find("eyeSP");
        charge.Stop();

        closePos = transform.position;
        openPos = transform.position + Vector3.down * openMax;
    }

    // Update is called once per frame
    void Update()
    {
        Light2D light2D = GetComponent<Light2D>();
        EyeSP_Move eyeSP_Move = eyeSP.GetComponent<EyeSP_Move>();

        if (!eyeSP_Move.appear)
        {
            bathe = false;
        }

        if (bathe)
        {
            light2D.intensity = 1;
        }
        else
        {
            light2D.intensity = 3;
        }

        Move();

        Camera_Move camera_Move = vCam.GetComponent<Camera_Move>();
        camera_Move.Shakes(ref shake, 0.15f, 3);
    }

    void Move()
    {
        Vector2 objPos = transform.position;
        Vector2 noise = Vector2.zero;

        // 開閉の動き
        if (!endOpen)
        {
            if (bathe)
            {
                // ゆっくりと開けていく
                if (moveTimer >= 0.4)
                {
                    objPos += Vector2.down * openSpeed * Time.deltaTime;
                }

                // オブジェクトを揺らす
                noise = new Vector2((Random.value * 2 - 1) * 0.06f, (Random.value * 2 - 1) * 0.01f);

                moveTimer += Time.deltaTime;
            }
            else
            {
                moveTimer = 0;
                objPos -= Vector2.down * closeSpeed * Time.deltaTime;
            }
        }


        // 開閉の上限
        float minX, maxX, minY, maxY;
        if (closePos.x > openPos.x)
        {
            minX = openPos.x;
            maxX = closePos.x;
        }
        else
        {
            minX = closePos.x;
            maxX = openPos.x;
        }

        if (closePos.y > openPos.y)
        {
            minY = openPos.y;
            maxY = closePos.y;
        }
        else
        {
            minY = closePos.y;
            maxY = openPos.y;
        }

        if (objPos.y < openPos.y)
        {
            endOpen = true;
            if (!shakeFlag) // 一度だけ振動のbool値をtrueにする。
            {
                open.Play();
                shake = true;
                shakeFlag = true;
                gogogoAudio.Stop();
                gateAudio.Play();
            }
        }
        objPos = new Vector2(Mathf.Clamp(objPos.x, minX, maxX), Mathf.Clamp(objPos.y, minY, maxY));


        transform.position = objPos + noise;


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("vision"))
        {
            charge.Play();
            gogogoAudio.Play();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("vision"))
        {
            bathe = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("vision"))
        {
            bathe = false;
            charge.Stop();
            gogogoAudio.Stop();
        }
    }
}
