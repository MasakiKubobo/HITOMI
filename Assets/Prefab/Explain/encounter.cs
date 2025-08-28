using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class encounter : MonoBehaviour
{
    public Transform player;
    public Light2D HPLight;

    public float startX, endX, encountX;

    [Space(5)]
    public GameObject eye, eyeSprite, eyeExplain;
    public ParticleSystem encount;
    private bool encountFlag;
    // Start is called before the first frame update
    void Start()
    {
        HPLight.intensity = 2;
        eyeExplain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!encountFlag)
        {
            if (player.position.x < startX) HPLight.intensity = 2;
            else if (player.position.x >= startX && player.position.x < endX)
            {
                float Length = endX - startX;
                float Advance = player.position.x - startX;

                float ratio = 1 - (Advance / Length);
                HPLight.intensity = ratio * 1.8f + 0.2f;
            }
            else if (player.position.x >= endX && player.position.x < encountX) HPLight.intensity = 0.2f;
            else if (player.position.x >= encountX)
            {
                encountFlag = true;
                StartCoroutine(Encount());
            }
        }

    }

    IEnumerator Encount()
    {
        encount.Play();
        eyeSprite.SetActive(false);

        float timer = 0.2f;
        while (timer < 2)
        {
            HPLight.intensity = timer;
            timer += 0.2f;
            yield return new WaitForSeconds(0.05f);
        }
        Eye_Move eye_Move = eye.GetComponent<Eye_Move>();
        eye_Move.tutorial = false;

        eyeExplain.SetActive(true);
    }
}
