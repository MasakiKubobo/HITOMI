using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class camera_move : MonoBehaviour
{
    // プレイヤーオブジェクトにこのスクリプトをアタッチ
    public GameObject Camera;
    public GameObject[] CameraPos;

    private Vector2 originPos;
    private int state = 0;
    private bool move = false;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            timer += Time.deltaTime;

            Camera.transform.position = Vector2.Lerp(originPos, CameraPos[state].transform.position, timer);
            Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, -10);

            if (timer >= 1)
            {
                move = false;
                timer = 0;
            }

        }
        else
        {
            originPos = Camera.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        int i = 0;
        while (i < CameraPos.Length)
        {
            if (other.gameObject == CameraPos[i])
            {
                state = i;
                move = true;
                break;
            }
            i++;
        }

    }
}
