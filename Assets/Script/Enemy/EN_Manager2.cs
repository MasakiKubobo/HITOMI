using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EN_Manager2 : MonoBehaviour
{
    public GameObject mtrObject;
    Materialize materialize;
    public bool loop = false;
    private bool loopFlag = false;
    public float delayTime = 0.5f;

    [System.Serializable]
    struct Enemys
    {
        public GameObject enPos;
        public GameObject enKind;
    }
    [SerializeField] Enemys[] enemys;
    public int EnemysCount;

    public GameObject effect;

    GameObject[] ENs;
    Vector2[] ENpos;
    private bool enSwitch = false, enFlag = false, mtrFlag = false;
    private float moveTimer = 0, delayTimer = 0, reTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        //materialize = mtrObject.GetComponent<Materialize>();

        ENs = new GameObject[EnemysCount];
        ENpos = new Vector2[EnemysCount];
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (materialize.mtr)
        {
            if (!mtrFlag)
            {
                enSwitch = false;
                enFlag = false;
                moveTimer = 0;
                delayTimer = 0;

                mtrFlag = true;
            }
        }
        else mtrFlag = false;
        */



        if (enSwitch)
        {

            if (!enFlag)
            {
                int index = 0;
                foreach (Enemys enemy in enemys) // 敵を出現させる
                {
                    Instantiate(effect, enemy.enPos.transform.position, Quaternion.identity);
                    ENs[index] = Instantiate(enemy.enKind, enemy.enPos.transform.position, Quaternion.identity);

                    ENpos[index] = enemy.enPos.transform.position;
                    index++;
                }
                enFlag = true;
            }

            if (moveTimer < 0.5) // 出現後一定時間硬直
            {
                int index = 0;
                foreach (Enemys enemy in enemys)
                {
                    ENs[index].transform.position = ENpos[index];
                    index++;
                }

                // 注意！enemy01の重力の値は下げること
            }
            else loopFlag = true;
            moveTimer += Time.deltaTime;
        }


        if (loop)
        {
            if (loopFlag)
            {
                enSwitch = false;
                enFlag = false;
                moveTimer = 0;
                delayTimer = 0;
                loopFlag = false;
            }
        }


    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("vision"))
        {
            delayTimer += Time.deltaTime;

            if (delayTimer >= delayTime)
            {
                enSwitch = true;
            }
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("vision"))
        {
            enSwitch = false;
            enFlag = false;
            moveTimer = 0;
            delayTimer = 0;
        }
    }

}
