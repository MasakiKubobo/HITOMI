using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Gate : MonoBehaviour
{
    public ParticleSystem charge;
    private GameObject eyeSP;

    public float openSpeed, closeSpeed;
    public float openMax;
    private Vector3 openPos, closePos;

    private bool bathe = false;
    [HideInInspector] public bool endOpen = false;
    private float moveTimer = 0;
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
                if (moveTimer >= 0.8)
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

        if (objPos.y < openPos.y) endOpen = true;
        objPos = new Vector2(Mathf.Clamp(objPos.x, minX, maxX), Mathf.Clamp(objPos.y, minY, maxY));


        transform.position = objPos + noise;
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
        }
    }
}
