using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RayHitReceiver
{
    void OnRaycastHit();
}

public class AppearObject : MonoBehaviour, RayHitReceiver
{
    public float AppearSpeed = 1; // 透明化が解ける速度
    public float HideSpeed = 1; // 透明化する速度
    public GameObject _collider; // 実体化すると現れる物理判定
    public GameObject _light; //実体化すると全体を表示させるための光
    public GameObject Aura_light; //実体化すると現れるオーラ

    private bool Materialized = false; //実体化状態か否か
    private bool appearing = false; // 透明化解除の最中か否か 

    private float alpha = 0;
    private SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = new Color(1, 1, 1, alpha);

        _collider.SetActive(false);
        Aura_light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Rayが当たっていたら透明化が解けていき、当たっていない場合は再び透明になっていく
        alpha += appearing ? Time.deltaTime * AppearSpeed : -Time.deltaTime * HideSpeed;

        alpha = alpha <= 0 ? 0 : alpha; // アルファ値が0より下に行かないようにする

        if (Materialized) // 実体化中は不透明になる
        {
            _renderer.color = new Color(1, 1, 1, 1); // 不透明にする
            _collider.SetActive(true); // 当たり判定をつける
            _light.SetActive(true); // 全体に光を当てる
            Aura_light.SetActive(true); // オーラを纏わせる
            alpha = 0;
        }
        else
        {
            _renderer.color = new Color(1, 1, 1, alpha);
            _collider.SetActive(false);
            _light.SetActive(false);
            Aura_light.SetActive(false);
            if (alpha >= 1) Materialized = true; // アルファ値が1を超えると実体化する
        }

        // 瞳を閉じると実体化が解除されるのでMaterializedはfalseとなる

        appearing = false;
    }

    public void OnRaycastHit()
    {
        Debug.Log($"{gameObject.name}がRayに当った");
        appearing = true;
    }
}
