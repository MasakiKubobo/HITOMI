using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFloors : MonoBehaviour
{
    public ParticleSystem rubbleEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Playerとの接触");

            rubbleEffect.Play();

            AudioSource audioSource = rubbleEffect.GetComponent<AudioSource>();

            Destroy(gameObject);

        }
    }
}
