using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private string GroundTag = "Ground";

    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }
        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;

    }


    private void OriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == GroundTag)
        {
            isGroundEnter = true;
            Debug.Log("何かが判定に入りました");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == GroundTag)
        {
            isGroundStay = true;
            Debug.Log("何かが判定に入り続けています");
        }
    }

    private void OTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == GroundTag)
        {
            isGroundExit = true;
            Debug.Log("何かが判定を出ました");
        }
    }
}
