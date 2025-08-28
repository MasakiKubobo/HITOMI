using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textAppear2 : MonoBehaviour
{
    public checkPoint checkpoint;
    public float upSpeed = 1;
    public float hideSpeed = 1;
    private TextMeshPro text;

    private GameObject player, eye;
    private float alphaTimer = 1;

    private bool _switch, switchFlag;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.color = new Color(1, 1, 1, 0);

        player = GameObject.Find("Player");
        eye = GameObject.Find("eye");
    }

    // Update is called once per frame
    void Update()
    {
        if (checkpoint.checkFlag) _switch = true;

        if (_switch)
        {
            text.color = new Color(1, 1, 1, alphaTimer);
            alphaTimer -= Time.deltaTime * hideSpeed;

            transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0);
        }
    }
}
