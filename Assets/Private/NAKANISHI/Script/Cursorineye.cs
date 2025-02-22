using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursoreye : MonoBehaviour
{
    bool Cineye;
    public void Cursorineye(Collider other)
    {
        if (other.gameObject.CompareTag("Eye"))
        {
            Cineye = true;
        }
        else
        {
            Cineye = false;
        }
    }
    private void CursorHitboxForEye()
    {

    }

}
