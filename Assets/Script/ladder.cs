using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    public GameObject Player, _player;
    private bool Onladder = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Onladder)
        {
            Debug.Log("eee");
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                Player.transform.position += new Vector3(Player.transform.position.x, 5 * Time.deltaTime, Player.transform.position.z);

            }
        }
    }

    void OnCollisionStay2D(Collision2D other) // 地面に足がついている時だけジャンプ可能
    {
        if (other.gameObject == _player) Onladder = true;
    }
    void OnCollisionExit2D(Collision2D other) // 地面が足が離れた時はジャンプ不可にする
    {

        //if (other.gameObject == _player) Onladder = false;
    }
}
