using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public bool isBox = false;
    public Rigidbody2D rb;
    private GameObject eye;
    public float powor;
    public AudioSource ironAudio;
    // Start is called before the first frame update
    void Start()
    {
        eye = GameObject.Find("eye");
        rb.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        Materialize materialize = GetComponent<Materialize>();

        if (materialize.Mtr)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            if (!isBox)
            {
                eye.transform.position = transform.position;
                eye.transform.rotation = transform.rotation;
            }
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
            if (!isBox) eye.transform.eulerAngles = Vector3.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PrefabID prefabID = other.gameObject.GetComponent<PrefabID>();
        if (prefabID != null)
        {
            if (prefabID.ID == "attack_01")
            {
                Materialize materialize = GetComponent<Materialize>();
                if (materialize.Mtr)
                {
                    KnockBack(other.transform.position, other.bounds.ClosestPoint(transform.position), powor);
                    ironAudio.Play();
                }
            }
        }
    }

    void KnockBack(Vector2 PLvec, Vector2 ConPos, float powor)
    {
        Vector2 vec = (Vector2)transform.position - PLvec;
        rb.AddForce(vec.normalized * powor, ForceMode2D.Impulse);
    }
}
