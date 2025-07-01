using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour
{
    public AudioClip main, climax;
    private AudioSource audioSource;

    private bool mainFlag, climaxFlag;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = main;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("vision"))
        {
            if (!climaxFlag)
            {
                audioSource.clip = climax;
                audioSource.Play();

                climaxFlag = true;
                mainFlag = false;
            }
        }

        if (other.CompareTag("Player"))
        {
            if (!mainFlag)
            {
                audioSource.clip = main;
                audioSource.Play();

                mainFlag = true;
                climaxFlag = false;
            }
        }
    }
}
