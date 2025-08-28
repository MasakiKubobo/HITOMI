using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class explain : MonoBehaviour
{
    public bool isEyeExplain, isItem;
    public bool Endline;
    public float lineX;
    [Space(10)]
    public ParticleSystem start, loop, end;

    [Space(10)]
    public SpriteRenderer[] sprites;
    public TextMeshPro text, text2;

    private bool effectFlag, endFlag;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            Color color = sprite.color;
            color.a = 0;
            sprite.color = color;
        }

        Color TEcolor = text.color;
        TEcolor.a = 0;
        text.color = TEcolor;
        if (text2 != null) text2.color = TEcolor;

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Endline)
        {
            if (player.transform.position.x > lineX)
            {
                if (!endFlag)
                {
                    StartCoroutine(EndExplain());
                    endFlag = true;
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isItem && !isEyeExplain && other.CompareTag("Player"))
        {
            if (!effectFlag)
            {
                StartCoroutine(StartExplain());
                effectFlag = true;
            }
        }
        if (!isItem && isEyeExplain && other.CompareTag("eye"))
        {
            if (!effectFlag)
            {
                StartCoroutine(StartExplain());
                effectFlag = true;
            }
        }
        if (isItem && !isEyeExplain && other.CompareTag("Item"))
        {
            if (!effectFlag)
            {
                StartCoroutine(StartExplain());
                effectFlag = true;
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!isItem && !isEyeExplain && other.CompareTag("Player"))
        {
            if (!Endline && !endFlag)
            {
                StartCoroutine(EndExplain());
                endFlag = true;
            }
        }
        if (!isItem && isEyeExplain && other.CompareTag("eye"))
        {
            if (!Endline && !endFlag)
            {
                StartCoroutine(EndExplain());
                endFlag = true;
            }
        }
        if (isItem && !isEyeExplain && other.CompareTag("Item"))
        {
            if (!Endline && !endFlag)
            {
                StartCoroutine(EndExplain());
                endFlag = true;
            }
        }
    }

    IEnumerator StartExplain()
    {
        start.Play();
        yield return new WaitForSeconds(0.5f);

        loop.Play();
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += 0.1f;

            foreach (SpriteRenderer sprite in sprites)
            {
                Color color = sprite.color;
                color.a = alpha;
                sprite.color = color;
            }
            Color TEcolor = text.color;
            TEcolor.a = alpha;
            text.color = TEcolor;
            if (text2 != null) text2.color = TEcolor;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator EndExplain()
    {
        end.Play();
        yield return new WaitForSeconds(0.5f);

        loop.Stop();
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= 0.07f;

            foreach (SpriteRenderer sprite in sprites)
            {
                Color color = sprite.color;
                color.a = alpha;
                sprite.color = color;
            }
            Color TEcolor = text.color;
            TEcolor.a = alpha;
            text.color = TEcolor;
            if (text2 != null) text2.color = TEcolor;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
