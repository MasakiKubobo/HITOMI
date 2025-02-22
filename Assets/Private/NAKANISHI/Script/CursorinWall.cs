using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class CursorinWall : MonoBehaviour
{
    public void Cursorwall(Collider other)
    {
        bool Cinwall;

        if (other.gameObject.CompareTag("Wall"))
        {
            Cinwall = true;
        }
        else
        {
            Cinwall = false;
        }


    }
    private void CursorHitboxForWall()
    {

    }



}
