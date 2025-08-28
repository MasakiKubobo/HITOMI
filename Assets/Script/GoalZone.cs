using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GoalZone : MonoBehaviour
{
    public Image black;
    public TextMeshProUGUI clear;

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Goals()
    {
        Time.timeScale = 0;
        Eye_HP.HP = 100;

        float timer = 0;
        while (timer < 1)
        {
            timer += Time.unscaledDeltaTime * 3;
            black.color = new Color(0, 0, 0, timer);
        }

        audioSource.Play();

        timer = 0;
        while (timer < 1)
        {
            timer += Time.unscaledDeltaTime * 2;
            clear.color = new Color(1, 1, 1, timer);
        }

        yield return new WaitForSecondsRealtime(3);
        if (SceneManager.GetActiveScene().name == "main0")
        {
            GameManager.pointer = 0;
            SceneManager.LoadScene("start");
        }
        else if (SceneManager.GetActiveScene().name == "main1")
        {
            GameManager.pointer = 0;
            SceneManager.LoadScene("start");
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Goals());
        }
    }
}
