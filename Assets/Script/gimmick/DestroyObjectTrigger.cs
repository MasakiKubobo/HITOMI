using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectTrigger : MonoBehaviour
{
    public List<GameObject> targetObjects; // インスペクターから破壊したいオブジェクトを複数設定するためのリスト

    public ParticleSystem rubbleEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーが接触した時のみ作動
        if (other.CompareTag("Player"))
        {
            rubbleEffect.Play();
            Debug.Log("Playerとの接触。指定されたオブジェクトを破壊します。");

            // リストに登録された全てのオブジェクトに対して処理を実行
            foreach (GameObject obj in targetObjects)
            {
                // オブジェクトが存在する場合のみ処理を行う（安全対策）
                if (obj != null)
                {
                    Destroy(obj);// 対象のオブジェクトを破壊する
                }
            }

            // このトリガーオブジェクト自身の役目は終わったので、非アクティブにする
            // Destroy(gameObject) で消しても良い
            gameObject.SetActive(false);
        }
    }
}