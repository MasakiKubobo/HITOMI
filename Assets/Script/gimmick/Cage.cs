using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public bool isBox = false;
    public Rigidbody2D rb;
    private GameObject eyeSP;
    // Start is called before the first frame update
    void Start()
    {
        eyeSP = GameObject.Find("eyeSP");
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
                eyeSP.transform.position = transform.position;
                eyeSP.transform.rotation = transform.rotation;
            }
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
            if (!isBox) eyeSP.transform.eulerAngles = Vector3.zero;
        }
    }
}
