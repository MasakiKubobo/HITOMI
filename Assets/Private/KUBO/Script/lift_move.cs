using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class lift_move : MonoBehaviour
{
    [Header("Speed and Direction")]
    public float X;
    public float Y;

    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AppearObject appearObject = GetComponent<AppearObject>();
        if (appearObject.Materialized) // もし実体化中なら
        {
            transform.position += new Vector3(X, Y) * Time.deltaTime;
        }
        else
        {
            transform.position = startPos;
        }
    }
}
