using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneMove : MonoBehaviour
{
    public GameObject GorlObject;
    public GameObject gorl;

    private string sceneName = "test1";
    // Start is called before the first frame update
    void Start()
    {
        gorl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("test1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("test2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("test3");
        }
    }

    void NextStage()
    {
        SceneManager.LoadScene(sceneName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Goal")
        {
            if (SceneManager.GetActiveScene().name == "test1") sceneName = "test2";
            if (SceneManager.GetActiveScene().name == "test2") sceneName = "test3";
            gorl.SetActive(true);
            Invoke(nameof(NextStage), 1f);
        }
    }
}
