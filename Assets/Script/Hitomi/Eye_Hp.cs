using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eye_HP : MonoBehaviour
{
    public Slider HPber;
    public GameObject HpLight;

    public static float HP = 100;
    public float StartHP = 100;
    public float Healing = 5;
    public float HealTime = 2;

    private float beforeHP, healTimer = 0;
    private bool damage = false;
    // Start is called before the first frame update
    void Start()
    {
        HP = StartHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP > 100) HP = 100;
        if (HP < 0) HP = 0;
        HPber.value = HP / 100;

        if (HP < beforeHP)
        {
            damage = true;
        }
        if (damage)
        {
            healTimer = HealTime;
            damage = false;
        }

        // ダメージを受けた一定時間後から回復が始まる。
        healTimer -= Time.deltaTime;
        if (healTimer <= 0) HP += Healing * Time.deltaTime;

        beforeHP = HP; // 関数の最後に
    }
}
