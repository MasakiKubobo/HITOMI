using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public GameObject vCam;
    public bool isUp = false;
    public float distance = 10, moveSpeed = 0.1f;
    public float moveTime = 10;

    private bool _switch = false, shake = false, shakeFlag = false;
    private float moveTimer = 0, liftTimer = 0;
    private float Y;
    private GameObject player;

    public AudioSource gogogoAudio, stopAudio;
    // Start is called before the first frame update
    void Start()
    {
        Y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (_switch) // 触れると数秒後に振動する
        {
            if (moveTimer >= moveTime)
            {
                if (!shakeFlag)
                {
                    gogogoAudio.Play();
                    shake = true;
                    shakeFlag = true;
                }
            }
            moveTimer += Time.deltaTime;
        }

        if (moveTimer >= moveTime + 0.5)
        {
            if (!isUp)
            {
                liftTimer += Time.deltaTime * moveSpeed;
                Mathf.Clamp01(liftTimer);

                float moveY = Mathf.Lerp(Y, Y + distance, liftTimer);
                transform.position = new Vector2(transform.position.x, moveY);

                if (liftTimer >= 1)
                {
                    shake = true;
                    gogogoAudio.Stop();
                    stopAudio.Play();
                    isUp = true;
                }
            }
            else
            {

            }
        }

        Camera_Move camera_Move = vCam.GetComponent<Camera_Move>();
        camera_Move.Shakes(ref shake, 1f, 4);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _switch = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
