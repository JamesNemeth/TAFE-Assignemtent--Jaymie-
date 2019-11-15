using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Image staminaFill;
    public float curStamina, maxStamina;

    bool staminaUsed;
    public float staminaPerSecond = 1f;

    private void Start()
    {
        InvokeRepeating("Timer", 1, 1f);
    }

    void StaminaChange()
    {
        float amount = Mathf.Clamp01(curStamina / maxStamina);
        staminaFill.fillAmount = amount;
    }
    void Update()
    {
        StaminaChange();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            curStamina -= 1;
        }

        if (curStamina > 100)
        {
            curStamina = 100;
        }
    }
    private void Timer()
    {
        curStamina += staminaPerSecond;
    }
}
