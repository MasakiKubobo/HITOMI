using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    [SerializeField] private InputAction button;

    public TextMeshProUGUI text;
    private float alpha = 0.5f, textTimer;
    private bool up = false;
    private bool buttonFlag;

    private void OnEnable()
    {
        button?.Enable();
    }
    private void Disable()
    {
        button?.Disable();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var _button = button.ReadValue<float>();

        if (_button <= 0)
        {
            buttonFlag = true;
        }
        if (_button >= 1)
        {
            if (buttonFlag) SceneManager.LoadScene("main0");
        }

        if (!up)
        {
            alpha = Mathf.Lerp(0.5f, 0, textTimer);

            textTimer += Time.deltaTime;

            if (textTimer >= 1)
            {
                up = true;
                textTimer = 0;
            }
        }
        else
        {
            alpha = Mathf.Lerp(0, 0.5f, textTimer);

            textTimer += Time.deltaTime;

            if (textTimer >= 1)
            {
                up = false;
                textTimer = 0;
            }
        }

        text.color = new Color(1, 1, 1, alpha);
    }
}
