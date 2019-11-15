﻿using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image HealthIcon;
    public float curHealth, maxHealth;

    [Header("Damage Effects")]

    public Image damageImage;
    public float flashSpeed = 5;
    public Color flashColor = new Color(1, 0, 0, 0.2f);
    bool damaged;

    void HealthChange()
    {
        float amount = Mathf.Clamp01(curHealth / maxHealth);
        HealthIcon.fillAmount = amount;
    }

    // Update is called once per frame
    void Update()
    {
        HealthChange();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            damaged = true;
            curHealth -= 5;
        }
        if (damaged)
        {
            damageImage.color = flashColor;
            damaged = false;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
    }
}