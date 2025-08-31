using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GoalZone : MonoBehaviour
{
    public Image black, nextImg;
    public TextMeshProUGUI clear, nextText;
    public GameObject EYE;
    public RectMask2D rectMask;
    public ParticleSystem appear, ap01, ap02, ap03, ap04, ap05;
    public AudioSource audioSource, as01, as02, as03, as04, as05;


    [HideInInspector] public bool next;
    // Start is called before the first frame update
    void Start()
    {
        nextImg.color = new Color(1, 1, 1, 0);
        nextText.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Goals()
    {
        Time.timeScale = 0;

        float timer = 15;
        while (timer > 0)
        {
            Vector4 vector4 = rectMask.padding;
            vector4.w += 20;
            rectMask.padding = vector4;

            EYE.transform.localScale = new Vector2(15, timer);
            timer -= 1f;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        EYE.transform.localScale = new Vector2(15, 0);

        appear.Play();
        timer = 0;
        bool audioFlag = false;
        while (timer < 1)
        {
            timer += 0.1f;
            clear.color = new Color(1, 1, 1, timer);

            if (timer >= 0.2 && !audioFlag)
            {
                audioSource.Play();
                audioFlag = true;
            }
            yield return new WaitForSecondsRealtime(0.05f);
        }
        ap01.Play();
        as01.Play();
        yield return new WaitForSecondsRealtime(0.2f);
        ap02.Play();
        as02.Play();
        yield return new WaitForSecondsRealtime(0.2f);
        ap03.Play();
        as03.Play();
        if (ap04 != null)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            ap04.Play();
            as04.Play();
        }
        if (ap05 != null)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            ap05.Play();
            as05.Play();
        }
        yield return new WaitForSecondsRealtime(1f);

        timer = 0;
        Color color = nextImg.color;

        while (true)
        {
            while (timer < 1)
            {
                if (next) break;

                timer += 0.1f;
                color.a = timer;
                nextImg.color = color;
                nextText.color = color;
                yield return new WaitForSecondsRealtime(0.05f);
            }
            while (timer > 0)
            {
                if (next) break;

                timer -= 0.1f;
                color.a = timer;
                nextImg.color = color;
                nextText.color = color;
                yield return new WaitForSecondsRealtime(0.05f);
            }

            if (next) break;
        }

        if (SceneManager.GetActiveScene().name == "main1")
        {
            GameManager.pointer = 0;
            SceneManager.LoadScene("main2");
        }
        else if (SceneManager.GetActiveScene().name == "main2")
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
