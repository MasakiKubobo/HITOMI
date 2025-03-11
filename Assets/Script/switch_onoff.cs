using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class switch_onoff : MonoBehaviour
{
    public bool switchOn = false;

    private Vector3 switchPos;
    // Start is called before the first frame update
    void Start()
    {
        switchPos = transform.position;

        if (switchOn) transform.position += new Vector3(0, -0.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform); // プレイヤーをリフトの子オブジェクト化
            transform.position = switchPos;
            transform.position += new Vector3(0, -0.5f, 0);

            switchOn = !switchOn;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            if (switchOn)
            {
                transform.position = switchPos;
                transform.position += new Vector3(0, -0.3f, 0);
            }
            else
            {
                transform.position = switchPos;
            }
        }
    }
}
