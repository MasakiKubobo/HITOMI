using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class camera_mask : MonoBehaviour
{
    public GameObject Mask;
    public float maskSpeed = 1;

    public static bool GameOver = false;
    private bool gameOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        GameOver = false;
        Mask.transform.localScale = new Vector3(15, 0, 1);
        Invoke(nameof(maskOpen), 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (gameOpen)
        {
            Mask.transform.localScale += new Vector3(0, maskSpeed * Time.deltaTime, 0);
            if (Mask.transform.localScale.y >= 14) gameOpen = false;
        }

        if (GameOver)
        {
            Debug.Log("ゲームオーバー");
            Mask.transform.localScale -= new Vector3(0, maskSpeed * Time.deltaTime, 0);
            if (Mask.transform.localScale.y <= 0)
            {
                Mask.transform.localScale = new Vector3(15, 0, 1);
                Invoke(nameof(maskClose), 0.1f);
            }
        }
    }

    void maskOpen()
    {
        gameOpen = true;
    }

    void maskClose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
