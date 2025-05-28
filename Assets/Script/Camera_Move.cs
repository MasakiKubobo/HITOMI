using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public GameObject player;
    public float Xmax = 50, Xmin = 0, Ymax = 10, Ymin = 0;

    private float X, Y;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        X = player.transform.position.x;
        Y = player.transform.position.y;

        if (Xmax <= player.transform.position.x) X = Xmax;
        if (Xmin >= player.transform.position.x) X = Xmin;
        if (Ymax <= player.transform.position.y) Y = Ymax;
        if (Ymin >= player.transform.position.y) Y = Ymin;

        transform.position = new Vector3(X, Y, -10);
    }
}
