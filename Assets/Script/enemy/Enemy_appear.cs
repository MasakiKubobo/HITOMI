using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_appear : MonoBehaviour
{
    public GameObject triggerObject; // 実体化のトリガーとなる実体化オブジェクト
    public float AppearSpeed = 1; // 透明化が解ける速度

    [Space(10)]
    public GameObject _light; //実体化すると全体を表示させるための光
    public GameObject Aura_light; //実体化すると現れるオーラ
    private Collider2D col;

    [HideInInspector] public bool Materialized = false; // 実体化状態か否か
    private float alpha = 0;

    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = new Color(1, 1, 1, 0);

        col = GetComponent<Collider2D>();
        col.enabled = false; // コライダーを非活性化
        _light.SetActive(false);
        Aura_light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // トリガーとなるオブジェクトの実体化スクリプトを取得
        AppearObject appearObject = triggerObject.GetComponent<AppearObject>();

        if (appearObject.Materialized)
        {
            // 対象オブジェクトが実体化した後、アルファ値を1まで上げていく
            alpha += alpha < 1 ? AppearSpeed * Time.deltaTime : 0;
        }
        else alpha = 0;

        _renderer.color = new Color(1, 1, 1, alpha);

        Materialized = alpha >= 1 ? true : false; // アルファ値が1の場合のみ実体化状態に

        if (Materialized)
        {
            // 実体化状態はコライダー・ライト・オーラを活性化
            col.enabled = true;
            _light.SetActive(true);
            Aura_light.SetActive(true);
        }
        else
        {
            col.enabled = false;
            _light.SetActive(false);
            Aura_light.SetActive(false);
        }

    }

}
