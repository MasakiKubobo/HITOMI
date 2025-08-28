using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] startPoints;

    public int point;
    public static int pointer;
    public static bool reset;
    public static Vector2 resetPos;

    public GameObject eye, cam;

    [Space(10)]
    private GameObject EYE;
    public Image black;
    public TextMeshProUGUI gameover;
    public AudioSource audioSource;

    [Space(5)]
    public RectMask2D rectMask;

    private bool startFlag, resetFlag;
    void Awake()
    {
        Eye_Move eye_Move = eye.GetComponent<Eye_Move>();
        EYE = GameObject.Find("EYE");

        if (point != 0) pointer = point;
        if (pointer >= 2) eye_Move.tutorial = false;

        if (!reset)
        {
            transform.position = startPoints[pointer].transform.position;
            if (!eye_Move.tutorial) eye.transform.position = startPoints[pointer].transform.position;
            cam.transform.position = startPoints[pointer].transform.position;
        }
        else
        {
            transform.position = resetPos;
            eye.transform.position = resetPos;
            cam.transform.position = resetPos;
            reset = false;
        }

        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10);

        Eye_HP.HP = 100;
        Eye_Move.attackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startFlag)
        {
            StartCoroutine(Start());
            startFlag = true;
        }

        if (Eye_HP.HP <= 0)
        {
            StartCoroutine(GameOver());
        }

        if (reset)
        {
            if (!resetFlag)
            {
                StartCoroutine(Reset());
                resetFlag = true;
            }
        }

        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }

    private IEnumerator Start()
    {
        Time.timeScale = 0;
        rectMask.padding = new Vector4(-100, 0, -1000, 0);

        float timer = 0;
        while (timer < 15)
        {
            EYE.transform.localScale = new Vector2(15, timer);
            timer += 1f;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        EYE.transform.localScale = new Vector2(15, 15);

        Time.timeScale = 1;

        timer = 0;
        while (timer > -250)
        {
            timer -= 10f;

            Vector4 vector4 = rectMask.padding;
            vector4.w = timer;
            rectMask.padding = vector4;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
    private IEnumerator Reset()
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
            yield return new WaitForSecondsRealtime(0.02f);
        }
        EYE.transform.localScale = new Vector2(15, 0);

        resetPos = transform.position;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator GameOver()
    {
        Time.timeScale = 0;
        Eye_HP.HP = 100;
        audioSource.Play();

        float timer = 0;
        while (timer < 1)
        {
            timer += Time.unscaledDeltaTime * 3;
            black.color = new Color(0, 0, 0, timer);
            gameover.color = new Color(1, 0, 0, timer);
        }

        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == startPoints[1])
        {
            pointer = 1;
        }

        if (other.gameObject == startPoints[2])
        {
            pointer = 2;
        }
    }
}
