using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    public GameObject Player;
    public float StartX, GoalX;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float X = Player.transform.position.x;

        if (Player.transform.position.x < StartX) X = StartX;
        if (Player.transform.position.x > GoalX) X = GoalX;

        transform.position = new Vector3(X, transform.position.y, transform.position.z);

    }
}
