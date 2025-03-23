using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitomi_move : MonoBehaviour
{
    public GameObject eye; // 瞳オブジェクト
    public GameObject eyeLight; // 瞳の視野
    public GameObject animator; // 瞳のアニメーション
    private Animator anim;

    [HideInInspector] public bool Active = false; // 瞳オブジェクトが非表示か否か
    [HideInInspector] public bool Deactivate = false; // 非活性化した瞬間か否か
    [HideInInspector] public bool gameOver = false; // ゲームオーバーが確定したか否か
    Vector2 hitomiPos;
    // Start is called before the first frame update
    void Start()
    {
        eye.SetActive(false);
        eyeLight.SetActive(false);
        anim = animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 瞳オブジェクトの2種類のコライダーを取得
        CapsuleCollider2D capsuleCollider = eye.GetComponent<CapsuleCollider2D>(); // 瞳全体の当たり判定
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>(); // 黒目の当たり判定

        if (!gameOver)
        {
            if (Input.GetMouseButtonDown(0)) // 画面がクリックされたら
            {
                hitomiPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // マウス座標を世界座標に変換
                transform.position = hitomiPos; // マウスの場所に瞳オブジェクトを移動
                anim.SetBool("eyeOpen", true);
                Invoke(nameof(Opening), 0.25f);
            }
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("eyeClose", true);
                eye.SetActive(false);
                eyeLight.SetActive(false);
                Active = false;
                Deactivate = true;
                Invoke(nameof(Closeing), 0.25f);
            }
        }

        if (camera_mask.GameOver)
        {
            eye.SetActive(false);
            eyeLight.SetActive(false);
        }

    }

    void Opening()
    {
        anim.SetBool("eyeOpen", false);
        eye.SetActive(true);
        eyeLight.SetActive(true);

        Active = true;
    }
    void Closeing()
    {
        anim.SetBool("eyeClose", false);
        Deactivate = false;
    }

    void Damaging()
    {
        eye.SetActive(false);
        anim.SetBool("eyeDown", true);
        Invoke(nameof(Crying), 0.45f);
    }
    void Crying()
    {
        anim.SetBool("eyeDown", false);
        camera_mask.GameOver = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground"))
        {
            // 出現した際、黒目の届く位置に壁オブジェクトがあれば消えてしまう。
            eye.SetActive(false);
            eyeLight.SetActive(false);
            Active = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Active)
            {
                eyeLight.SetActive(false);
                gameOver = true;
                Invoke(nameof(Damaging), 0.5f);
                Active = false;
            }
        }
    }

}
