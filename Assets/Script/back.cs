using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Layers
{
    public GameObject obj;
    public float speed;
}

public class back : MonoBehaviour
{
    public GameObject Camera;
    public Layers[] layers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.zero;

        foreach (Layers layer in layers)
        {
            if (layer.obj != null)
            {
                Vector3 vec = layer.obj.transform.localPosition;
                vec.x = Camera.transform.position.x * layer.speed;
                layer.obj.transform.localPosition = vec;
            }
        }
    }
}
