using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PL_Controller : MonoBehaviour
{
    [SerializeField] private InputAction Dash;
    [SerializeField] private InputAction Jump;
    [SerializeField] private InputAction Attack;

    bool attackFlag = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        Dash?.Enable();
        Jump?.Enable();
        Attack?.Enable();
    }

    private void OnDisable()
    {
        Dash?.Disable();
        Jump?.Disable();
        Attack?.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var _Dash = Dash.ReadValue<float>();
        var _Jump = Jump.ReadValue<float>();
        var _Attack = Attack.ReadValue<float>();

        PL_Move pL_Move = GetComponent<PL_Move>();
        PL_Attack pL_Attack = GetComponent<PL_Attack>();


        if (_Dash > 0)
        {
            pL_Move.dash = true;
            pL_Move.left = true;
        }
        else if (_Dash < 0)
        {
            pL_Move.dash = true;
            pL_Move.left = false;
        }
        else
        {
            pL_Move.dash = false;
        }

        if (_Jump > 0)
        {
            pL_Move.jump = true;
        }
        else
        {
            pL_Move.jump = false;
        }

        if (_Attack > 0)
        {
            if (!attackFlag)
            {
                pL_Attack.attack = true;
                attackFlag = true;
            }
        }
        else attackFlag = false;


        /*
        if (Input.GetKey(KeyCode.D))
        {
            pL_Move.dash = true;
            pL_Move.left = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            pL_Move.dash = true;
            pL_Move.left = false;
        }
        else
        {
            pL_Move.dash = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            pL_Move.jump = true;
        }
        else
        {
            pL_Move.jump = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (!attackFlag)
            {
                pL_Attack.attack = true;
                attackFlag = true;
            }
        }
        else attackFlag = false;

        */



    }
}
