using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchAppear : MonoBehaviour
{
    public GameObject switchObject;

    [Space(5)]
    public bool Appear = false; // 非実体化状態でもうっすら見えるか否か
    public float StartAlpha = 0; // 非実体化状態でどれだけはっきり見えるか

    [Space(10)]
    private Collider2D _collider; // 実体化すると現れる物理判定
    public GameObject _light; //実体化すると全体を表示させるための光
    public GameObject Aura_light; //実体化すると現れるオーラ

    [HideInInspector] public bool Materialized = false;
    private SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        if (Appear) _renderer.color = new Color(1, 1, 1, StartAlpha);
        else _renderer.color = new Color(1, 1, 1, 0);

        if (_collider != null) _collider.enabled = false;
        if (Appear) _light.SetActive(true);
        else _light.SetActive(false);
        Aura_light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch_onoff switchMove = switchObject.GetComponent<switch_onoff>();

        if (switchMove.switchOn) // 実体化中は不透明になる
        {
            _renderer.color = new Color(1, 1, 1, 1); // 不透明にする
            if (_collider != null) _collider.enabled = true; // 当たり判定をつける

            _light.SetActive(true); // 全体に光を当てる
            Aura_light.SetActive(true); // オーラを纏わせる

            Materialized = true;
        }
        else
        {
            if (Appear) _renderer.color = new Color(1, 1, 1, StartAlpha);
            else _renderer.color = new Color(1, 1, 1, 0);

            if (_collider != null) _collider.enabled = false;
            if (Appear) _light.SetActive(true);
            else _light.SetActive(false);
            Aura_light.SetActive(false);

            Materialized = false;
        }
    }
}
