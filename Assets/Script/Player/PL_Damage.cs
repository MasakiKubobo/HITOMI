using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Damage : MonoBehaviour
{
    public float damageTime = 0.3f;
    public float invincibleTime = 0.6f;

    private float timer = 0;
    private bool invincible = false;
    [HideInInspector] public bool damage = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= damageTime)
        {
            damage = false;
        }
        if (timer >= invincibleTime)
        {
            invincible = false;
            timer = 0;
        }

        if (invincible) timer += Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PrefabID prefabID = other.gameObject.GetComponent<PrefabID>();
        if (prefabID == null) return;

        switch (prefabID.ID)
        {
            case "enemy_01":
                KnockBack(other.transform.position, 0);
                break;
            case "enemy_02":
                KnockBack(other.transform.position, 0);
                break;
        }
    }

    void KnockBack(Vector2 ENvec, float powor)
    {
        //Vector2 vec = (Vector2)eye.transform.position - ENvec;

        if (!invincible)
        {
            //rb.AddForce(vec.normalized * powor, ForceMode2D.Impulse);
            damage = true;
            invincible = true;
        }
    }
}
