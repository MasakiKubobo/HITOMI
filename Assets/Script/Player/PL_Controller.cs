using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PL_Controller : MonoBehaviour
{
    public bool Control = true;
    public bool down = false;
    public bool _Debug = true; // デバッグモードの場合キーマウ操作に

    private Gamepad[] pads;
    [SerializeField] private InputAction Dash, Up;
    [SerializeField] private InputAction Jump;
    [SerializeField] private InputAction Attack;

    [Space(5)]

    [SerializeField] private InputAction Rstick;
    [SerializeField] private InputAction eyeOpen;
    [SerializeField] private InputAction back;
    [SerializeField] private InputAction reset;

    //public GameObject animManager;
    public GameObject eye;

    public static bool haveItem, usingItem;
    public GameObject itemMark;
    //public GameObject kuromeSP; // キーマウ操作用


    private float backTimer = 0, deathTimer = 0;

    bool attackFlag = false, PLcon = true, PLconFlag;

    public AudioSource itemAudio;

    public GoalZone goalZone;
    // Start is called before the first frame update
    void Start()
    {
        pads = Gamepad.all.ToArray();
    }

    private void OnEnable()
    {
        Dash?.Enable();
        Up?.Enable();
        Jump?.Enable();
        Attack?.Enable();
        Rstick?.Enable();
        eyeOpen?.Enable();
        back?.Enable();
        reset?.Enable();
    }

    private void OnDisable()
    {
        Dash?.Disable();
        Up?.Disable();
        Jump?.Disable();
        Attack?.Disable();
        Rstick?.Disable();
        eyeOpen?.Disable();
        back?.Disable();
        reset?.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var _Dash = Dash.ReadValue<float>();
        var _Jump = Jump.ReadValue<float>();
        var _Attack = Attack.ReadValue<float>();

        var _Rstick = Rstick.ReadValue<Vector2>();
        var _reset = reset.ReadValue<float>();
        */

        /*var change = pads[0].leftStickButton.ReadValue();
        if (change == 1)
        {
            if (!PLconFlag)
            {
                PLcon = !PLcon;
                PLconFlag = true;
            }
        }
        else PLconFlag = false;
        */

        var _Dash = pads[0].leftStick.x.ReadValue();
        var _Jump = pads[0].buttonSouth.ReadValue();
        var _Attack = pads[0].buttonWest.ReadValue();
        var _Hand = pads[0].buttonNorth.ReadValue();
        var _Throw = pads[0].leftStick.ReadValue();

        var EyeLstick = pads[1].leftStick.ReadValue();
        var EyeRstick = pads[1].rightStick.ReadValue();
        var EyeRtrigger = pads[1].rightTrigger.ReadValue();

        var plResetL1 = pads[0].leftShoulder.ReadValue();
        var plResetL2 = pads[0].leftTrigger.ReadValue();
        var eyeResetL1 = pads[1].leftShoulder.ReadValue(); // 2つ目用
        var eyeResetL2 = pads[1].leftTrigger.ReadValue(); // 2つ目用

        var plNext = pads[0].buttonEast.ReadValue();
        var eyeNext = pads[1].buttonEast.ReadValue(); // 2つ目用

        if (plNext >= 1 || eyeNext >= 1) goalZone.next = true;
        else goalZone.next = false;

        /*
        if (PLcon)
        {
            EyeLstick = Vector2.zero;
            EyeRstick = Vector2.zero;
            EyeRtrigger = 0;
        }
        else
        {
            _Dash = 0;
            _Jump = 0;
            _Attack = 0;
            _Hand = 0;
            _Throw = Vector2.zero;
        }
        */


        PL_Move pL_Move = GetComponent<PL_Move>();
        PL_Attack pL_Attack = GetComponent<PL_Attack>();
        PL_Damage pL_Damage = GetComponent<PL_Damage>();
        PL_Motion pL_Motion = GetComponent<PL_Motion>();

        Eye_Anim eye_Anim = eye.GetComponent<Eye_Anim>();
        Eye_Move eye_Move = eye.GetComponent<Eye_Move>();
        //Eye_Anim eye_Anim = animManager.GetComponent<Eye_Anim>();
        //EyeSP_Anim eyeSP_Anim = animManager.GetComponent<EyeSP_Anim>();
        //EyeSP_Move eyeSP_Move = eyeSP.GetComponent<EyeSP_Move>();

        //EyeSPpointer eyeSPpointer = pointer.GetComponent<EyeSPpointer>();

        if (eye_Move.tutorial)
        {
            EyeLstick = Vector2.zero;
            EyeRstick = Vector2.zero;
            EyeRtrigger = 0;
        }

        if (!Control)
        {
            _Dash = 0;
            _Jump = 0;
            _Attack = 0;
            EyeRstick = Vector2.zero;
        }

        pL_Motion.down = down;

        if (_Dash > 0.5f)
        {
            pL_Move.dash = true;
            pL_Move.left = true;
        }
        else if (_Dash < -0.5f)
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
                //if (_Up > 0.5f) pL_Attack.attackUp = true;
                //else pL_Attack.attackFront = true;
                attackFlag = true;
            }
        }
        else attackFlag = false;

        // アイテムを掴む/投げる
        if (_Hand > 0)
        {
            pL_Move.hand = true;
        }
        else pL_Move.hand = false;

        if (_Throw.magnitude >= 0.6) pL_Move.throwPos = _Throw;
        else pL_Move.throwPos = Vector2.zero;

        if (pL_Damage.damage)
        {
            pL_Move.dash = false;
            pL_Move.jump = false;
            pL_Attack.attack = false;
        }

        // 瞳の移動
        if (EyeLstick.magnitude > 0.4) // スティックの傾きが0.4以上で有効に
        {
            eye_Move.movePos = EyeLstick;
        }
        else eye_Move.movePos = Vector2.zero;

        // 能力使用と解除
        if (EyeRstick.magnitude > 0.8) // スティックの傾きが0.8以上で有効に
        {
            eye_Anim.eyeAbility = true;
            eye_Move.kuromePos = EyeRstick;
        }
        else
        {
            eye_Anim.eyeAbility = false;
        }

        // 瞳の攻撃
        if (EyeRtrigger > 0)
        {
            eye_Move.attack = true;
        }
        else eye_Move.attack = false;

        /*
        // 特殊な瞳が一定以上離れるとスリップダメージ
        if (eye_Anim.eyeAbility)
        {
            Vector2 PLvec = transform.position;
            Vector2 EyeVec = eye.transform.position;
            if ((PLvec - EyeVec).magnitude >= 15)
            {
                Eye_HP.HP -= Time.deltaTime * 10;
            }
        }
        */

        if (plNext >= 1 && eyeNext >= 1)
        {
            backTimer += Time.deltaTime;
            if (backTimer >= 1) { GameManager.pointer = 0; SceneManager.LoadScene("start"); }
        }
        else backTimer = 0;

        // 積んだ時用のリセット処理
        if (plResetL1 >= 1 || plResetL2 >= 1 || eyeResetL1 >= 1 || eyeResetL2 >= 1)
        {
            GameManager.reset = true;
        }


        // キーマウ操作（デバッグ用）
        if (_Debug)
        {
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
                if (!pL_Damage.damage)
                {
                    if (!attackFlag)
                    {
                        pL_Attack.attack = true;
                        attackFlag = true;
                    }
                }

            }
            else attackFlag = false;
        }
    }
}
