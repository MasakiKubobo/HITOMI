using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWall : MonoBehaviour
{
    public SwitchButton switchButton;
    public GameObject wall, effectPos, effect;
    public float speed = 2f;

    private float down, sway;
    private bool upFlag, downFlag;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (switchButton.isSwitchOn)
        {
            down -= Time.deltaTime * speed;
            //sway = Mathf.PingPong(Time.time * 5, 0.2f) - 0.1f;
        }
        else
        {
            down += Time.deltaTime * speed;
            //sway = 0;
        }

        down = Mathf.Clamp(down, -4.5f, 0);
        wall.transform.localPosition = new Vector2(sway, down);

        if (down <= -4.3f)
        {
            if (!downFlag)
            {
                Instantiate(effect, effectPos.transform.position, Quaternion.identity);
                downFlag = true;
            }
        }
        else
        {
            downFlag = false;
        }
    }
}
