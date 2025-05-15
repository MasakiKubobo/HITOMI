using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PL_Attack : MonoBehaviour
{
    [HideInInspector] public bool attack;
    public float attackLimit = 0.3f;

    public Sprite normalSP, attackSP;
    private SpriteRenderer spriteRenderer;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            spriteRenderer.sprite = attackSP;
            timer += Time.deltaTime;
            if (timer >= attackLimit) attack = false;
        }
        else
        {
            spriteRenderer.sprite = normalSP;
            timer = 0;
        }

    }
}
