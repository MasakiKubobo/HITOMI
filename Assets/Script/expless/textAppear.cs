using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textAppear : MonoBehaviour
{
    public float appearSpeed = 1;
    public float playerX, playerY = -100, secondX;
    public bool secondIsX;
    public string second;
    private TextMeshPro text;
    private SpriteRenderer spriteRenderer;

    private GameObject player, eye;
    private float alphaTimer;

    private bool _switch, switchFlag;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        if (text != null) text.color = new Color(1, 1, 1, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.color = new Color(1, 1, 1, 0);

        player = GameObject.Find("Player");
        eye = GameObject.Find("eye");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_switch && player.transform.position.x >= playerX && player.transform.position.y >= playerY)
        {
            alphaTimer += Time.deltaTime * appearSpeed;
            if (text != null) text.color = new Color(1, 1, 1, alphaTimer);
            if (spriteRenderer != null) spriteRenderer.color = new Color(1, 1, 1, alphaTimer);


            if (!switchFlag)
            {
                if (!secondIsX)
                {
                    Eye_Anim eye_Anim = eye.GetComponent<Eye_Anim>();
                    if (eye_Anim.eyeAbility)
                    {
                        _switch = true;
                        switchFlag = true;
                    }
                }
                else
                {
                    if (player.transform.position.x >= secondX)
                    {
                        _switch = true;
                        switchFlag = true;
                    }
                }

            }
        }

        if (_switch)
        {
            alphaTimer -= Time.deltaTime * appearSpeed;
            if (text != null) text.color = new Color(1, 1, 1, alphaTimer);

            if (alphaTimer <= 0)
            {
                if (text != null) text.text = second;
                _switch = false;
            }

        }

        alphaTimer = Mathf.Clamp01(alphaTimer);

    }
}
