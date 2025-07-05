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

    public GameObject eye, cam;

    [Space(10)]
    public Image black;
    public TextMeshProUGUI gameover;
    public AudioSource audioSource;

    void Awake()
    {
        if (point != 0) pointer = point;
        transform.position = startPoints[pointer].transform.position;
        eye.transform.position = startPoints[pointer].transform.position;

        cam.transform.position = startPoints[pointer].transform.position;
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Eye_HP.HP <= 0)
        {
            StartCoroutine(GameOver());
        }

        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
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
