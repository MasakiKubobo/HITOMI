using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public GameObject Wind;
    public GameObject sprite;
    public float rollSpeed = 1;

    private float windTimer = 0;
    private bool windFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        Wind.SetActive(false);
        Wind.transform.localScale = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Materialize materialize = GetComponent<Materialize>();

        if (materialize.Mtr)
        {
            if (!windFlag)
            {
                Wind.SetActive(true);
                windFlag = true;
            }
            sprite.transform.Rotate(0, 0, rollSpeed * Time.deltaTime, Space.World);

            // 風エフェクトの動きに合わせて当たり判定を大きさ1まで拡大
            windTimer += Time.deltaTime;
            if (windTimer > 1) windTimer = 1;
            Wind.transform.localScale = new Vector3(windTimer, windTimer, 1);
        }
        else
        {
            windTimer = 0;
            Wind.transform.localScale = new Vector3(0, 0, 1);
            Wind.SetActive(false);

            windFlag = false;
        }
    }

}
