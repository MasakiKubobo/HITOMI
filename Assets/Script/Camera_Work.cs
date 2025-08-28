using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Work : MonoBehaviour
{
    public float Ymax, Ymin;
    public GameObject centerObj;
    [HideInInspector] public Vector2 centerPos;
    [HideInInspector] public bool isCenter = false;

    public GameObject lift;
    // Start is called before the first frame update
    void Start()
    {
        if (centerObj != null) centerPos = centerObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (lift != null)
        {
            Ymin = lift.transform.position.y + 4.5f;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("camera"))
        {
            if (centerObj != null)
            {

                if (12 >= centerPos.x - other.transform.position.x)
                {
                    isCenter = true;
                }
                else if (1 >= centerPos.x - other.transform.position.x)
                {
                    isCenter = false;
                }
            }
        }
    }
}
