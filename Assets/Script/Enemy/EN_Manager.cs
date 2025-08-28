using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EN_Manager : MonoBehaviour
{
    public float time;
    private float timer;
    public GameObject[] enemys;
    public GameObject[] enemys2;
    public bool isTime = false;
    public int twoCount = 1;
    public float twoTime = 10;

    public GameObject effect;

    private Vector2[] enPoses;
    private Vector2[] en2Poses;
    private bool enSwitch = false, en2Switch = false, enFlag = false, en2Flag = false;
    private float moveTimer = 0, moveTimer2 = 0, twoTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        foreach (GameObject enemy in enemys)
        {
            enemy.SetActive(false);
            index++;
        }
        enPoses = new Vector2[index + 1];

        index = 0;
        foreach (GameObject enemy in enemys2)
        {
            enemy.SetActive(false);
            index++;
        }
        en2Poses = new Vector2[index + 1];
    }

    // Update is called once per frame
    void Update()
    {
        if (enSwitch)
        {
            timer += Time.deltaTime;
            if (time <= timer)
            {

                if (!enFlag)
                {
                    int index = 0;
                    foreach (GameObject enemy in enemys) // 敵を出現させる
                    {
                        Instantiate(effect, enemy.transform.position, Quaternion.identity);
                        enemy.SetActive(true);

                        enPoses[index] = enemy.transform.position;
                        index++;
                    }
                    enFlag = true;
                }

                if (moveTimer < 0.5) // 出現後一定時間硬直
                {
                    int index = 0;
                    foreach (GameObject enemy in enemys)
                    {
                        enemy.transform.position = enPoses[index];
                        index++;
                    }

                    // 注意！enemy01の重力の値は下げること
                }
                moveTimer += Time.deltaTime;


                if (enemys2 != null)
                {
                    if (!isTime) // 生き残った数で第二波を始める
                    {
                        if (twoCount >= EnCount(enemys)) en2Switch = true;
                    }
                    else    // 時間で第二波を始める
                    {
                        if (twoTimer >= twoTime) en2Switch = true;
                    }
                    twoTimer += Time.deltaTime;
                }
            }
        }

        if (en2Switch)  // 第二波
        {
            if (!en2Flag)
            {
                int index = 0;
                foreach (GameObject enemy in enemys2) // 敵を出現させる
                {
                    Instantiate(effect, enemy.transform.position, Quaternion.identity);
                    enemy.SetActive(true);

                    en2Poses[index] = enemy.transform.position;
                    index++;
                }
                en2Flag = true;
            }

            if (moveTimer2 < 0.5) // 出現後一定時間硬直
            {
                int index = 0;
                foreach (GameObject enemy in enemys2)
                {
                    enemy.transform.position = en2Poses[index];
                    index++;
                }

                // 注意！enemy01の重力の値は下げること
            }
            moveTimer2 += Time.deltaTime;
        }
    }

    int EnCount(GameObject[] enemys) // 生き残っている敵の数
    {
        int count = 0;

        foreach (GameObject enemy in enemys)
        {
            if (enemy != null) count++;
        }

        return count;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("eye"))
        {
            enSwitch = true;
        }
    }
}
