using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class TriggerForCamera : MonoBehaviour
{
    public Camera_Move cameraMove;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // センサーに触れたのが "Player" なら
        if (other.CompareTag("Player"))
        {
            // カメラ監督に「開始！」の合図を送る
            cameraMove.StartCameraSequence();
            // センサーの役目は終わったのでOFFにする
            gameObject.SetActive(false);
        }
    }
}