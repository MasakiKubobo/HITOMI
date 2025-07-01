using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameObject black, rePosition;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        black.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator BlackOut()
    {
        black.SetActive(true);
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(0.5f);

        player.transform.position = rePosition.transform.position;
        black.SetActive(false);
        Eye_HP.HP = 15;
        Time.timeScale = 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            StartCoroutine(BlackOut());
        }
    }
}
