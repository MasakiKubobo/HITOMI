using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheapGate : MonoBehaviour
{
    public bool isVectors = true;
    public float X, downY;

    private GameObject eye;
    private float Y, timer = 0;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        eye = GameObject.Find("eye");
        Y = transform.position.y;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eye.transform.position.x >= X)
        {
            timer += Time.deltaTime * 6;
            Mathf.Clamp01(timer);
            if (timer >= 1) audioSource.Play();

            float down = Mathf.Lerp(Y, Y - downY, timer);
            transform.position = new Vector2(transform.position.x, down);
        }
    }
}
