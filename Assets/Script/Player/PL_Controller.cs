using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PL_Controller : MonoBehaviour
{
    [SerializeField] private InputAction Dash;
    [SerializeField] private InputAction Jump;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        Dash?.Enable();
        Jump?.Enable();
    }

    private void OnDisable()
    {
        Dash?.Disable();
        Jump?.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var _Dash = Dash.ReadValue<float>();
        var _Jump = Jump.ReadValue<float>();

        if (_Dash > 0)
        {
            Debug.Log(_Dash);
        }
        if (_Jump != 0)
        {
            Debug.Log(_Jump);
        }
    }
}
