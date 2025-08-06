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
    [SerializeField] private InputAction Dash, Up;
    [SerializeField] private InputAction Jump;
    [SerializeField] private InputAction Attack;

    [Space(5)]

    [SerializeField] private InputAction Rstick;
    [SerializeField] private InputAction eyeOpen;
    [SerializeField] private InputAction eyeItem;
    [SerializeField] private InputAction back;
    [SerializeField] private InputAction death;

    //public GameObject animManager;
    public GameObject eye;

    public static bool haveItem, usingItem;
    public GameObject itemMark;
    //public GameObject kuromeSP; // キーマウ操作用


    private float backTimer = 0, deathTimer = 0;

    bool attackFlag = false, appearFlag = false;

    public AudioSource itemAudio;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        Dash?.Enable();
        Up?.Enable();
        Jump?.Enable();
        Attack?.Enable();
        Rstick?.Enable();
        eyeOpen?.Enable();
        eyeItem?.Enable();
        back?.Enable();
        death?.Enable();
    }

    private void OnDisable()
    {
        Dash?.Disable();
        Up?.Disable();
        Jump?.Disable();
        Attack?.Disable();
        Rstick?.Disable();
        eyeOpen?.Disable();
        eyeItem?.Disable();
        back?.Disable();
        death?.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var _Dash = Dash.ReadValue<float>();
        var _Up = Up.ReadValue<float>();
        var _Jump = Jump.ReadValue<float>();
        var _Attack = Attack.ReadValue<float>();
        var _Rstick = Rstick.ReadValue<Vector2>();
        var _eyeOpen = eyeOpen.ReadValue<float>();
        var _eyeItem = eyeItem.ReadValue<float>();
        var _back = back.ReadValue<float>();
        var _death = death.ReadValue<float>();

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

        if (!Control)
        {
            _Dash = 0;
            _Jump = 0;
            _Attack = 0;
            _Rstick = Vector2.zero;
            _eyeOpen = 0;
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

        if (_Jump > 0.1)
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

        if (pL_Damage.damage)
        {
            pL_Move.dash = false;
            pL_Move.jump = false;
            pL_Attack.attack = false;
        }

        // 能力使用と解除
        if (Math.Sqrt(_Rstick.x * _Rstick.x + _Rstick.y * _Rstick.y) > 0.7) // スティックの傾きが0.5以上で有効に
        {
            eye_Anim.eyeAbility = true;
            eye_Move.kuromePos = _Rstick;
        }
        if (_eyeOpen >= 0.5)
        {
            eye_Anim.eyeAbility = false;
        }

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

        itemMark.SetActive(haveItem);

        if (haveItem && _eyeItem >= 1)
        {
            if (!eye_Anim.eyeAbility)
            {
                if (!eye_Anim.eyeAbility)
                {
                    eye_Anim.eyeAbility = true; // 能力を使用
                }
            }
            usingItem = true;
            haveItem = false;
        }

        if (_back >= 1)
        {
            backTimer += Time.deltaTime;
            if (backTimer >= 2) { GameManager.pointer = 0; SceneManager.LoadScene("start"); }
        }
        else backTimer = 0;

        if (_death >= 1)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= 2) Eye_HP.HP = 0;
        }
        else deathTimer = 0;


        // キーマウ操作（デバッグ用）
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

        if (Input.GetMouseButton(1))
        {
            if (!appearFlag && eyeSPpointer.canSummon)
            {
                if (!eyeSP_Anim.appearEye) eyeSP.transform.position = pointer.transform.position;

                eye_Anim.appearEye = !eye_Anim.appearEye;       // 押すごとに反転する
                eyeSP_Anim.appearEye = !eyeSP_Anim.appearEye;   // 押すごとに反転する

                appearFlag = true;
            }
        }
        else if (_eyeOpen == 0) appearFlag = false;

        // 特殊な瞳を開く方向
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pointer.transform.position = (Vector2)transform.position + pointerVec;
        if (!eyeSP_Anim.appearEye)
        {
            pointerVec = (mousePos - (Vector2)transform.position).normalized * 2;
            pointer.SetActive(true);

        }
        if (eyeSP_Anim.appearEye) pointer.SetActive(false);

        // 特殊な瞳の操作
        if (eyeSP_Anim.appearEye)
        {
            eyeSP_Move.appear = true;
            eyeSP_Move.kuromePos = (mousePos - (Vector2)eyeSP.transform.position).normalized;
        }
        else eyeSP_Move.appear = false;

        eyeSP.transform.eulerAngles = Vector3.zero;
        kuromeSP.transform.localScale = new Vector3(0, 0.8f, kuromeSP.transform.localScale.z);
        */

    }

    /* アイテム取る用
    void OnTriggerEnter2D(Collider2D other)
    {
        PrefabID prefabID = other.gameObject.GetComponent<PrefabID>();
        if (prefabID != null)
        {
            if (prefabID.ID == "item_01")
            {
                itemAudio.Play();
                haveItem = true;
                Destroy(other.gameObject);
            }
        }
    }
    */
}
