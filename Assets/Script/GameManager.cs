using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public GameObject[] startPoints;

    public int point;
    public static int pointer;

    public GameObject eye;

    void Awake()
    {
        if (point != 0) pointer = point;
        transform.position = startPoints[pointer].transform.position;
        eye.transform.position = startPoints[pointer].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Eye_HP.HP <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
