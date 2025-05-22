using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PL_Controller : MonoBehaviour
{
    [SerializeField] private InputAction Dash;
    [SerializeField] private InputAction Jump;
    [SerializeField] private InputAction Attack;

    [Space(5)]

    [SerializeField] private InputAction Rstick;
    [SerializeField] private InputAction eyeOpen;

    public GameObject animManager;
    public GameObject eye, eyeSP;


    private float timer = 0, timer2 = 0;

    bool attackFlag = false, appearFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        eyeSP.SetActive(false);
    }

    private void OnEnable()
    {
        Dash?.Enable();
        Jump?.Enable();
        Attack?.Enable();
        Rstick?.Enable();
        eyeOpen?.Enable();
    }

    private void OnDisable()
    {
        Dash?.Disable();
        Jump?.Disable();
        Attack?.Disable();
        Rstick?.Disable();
        eyeOpen?.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var _Dash = Dash.ReadValue<float>();
        var _Jump = Jump.ReadValue<float>();
        var _Attack = Attack.ReadValue<float>();
        var _Rstick = Rstick.ReadValue<Vector2>();
        var _eyeOpen = eyeOpen.ReadValue<float>();

        PL_Move pL_Move = GetComponent<PL_Move>();
        PL_Attack pL_Attack = GetComponent<PL_Attack>();
        PL_Damage pL_Damage = GetComponent<PL_Damage>();

        Eye_Anim eye_Anim = animManager.GetComponent<Eye_Anim>();
        EyeSP_Anim eyeSP_Anim = animManager.GetComponent<EyeSP_Anim>();


        if (_Dash > 0.3f)
        {
            pL_Move.dash = true;
            pL_Move.left = true;
        }
        else if (_Dash < -0.3f)
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

        if (pL_Damage.damage)
        {
            pL_Move.dash = false;
            pL_Move.jump = false;
            pL_Attack.attack = false;
        }

        if (_eyeOpen >= 0.5)
        {
            if (!appearFlag)
            {
                eye_Anim.appearEye = !eye_Anim.appearEye;
                eyeSP_Anim.appearEye = !eyeSP_Anim.appearEye; // 押すごとに反転する
                appearFlag = true;
            }
        }
        else if (_eyeOpen == 0) appearFlag = false;


        if (eye_Anim.appearEye)
        {
            eye.SetActive(true);
            if (timer >= 0.3) eyeSP.SetActive(false); // アニメーションが終わったら非表示にする
            timer += Time.deltaTime;
            timer2 = 0;
        }
        else
        {
            if (timer2 >= 0.3) eye.SetActive(false); // アニメーションが終わったら非表示にする
            eyeSP.SetActive(true);
            timer = 0;
            timer2 += Time.deltaTime;
        }

        /* キーボード入力用
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
