using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerStats
    {
        public string name;
        public int value;
    }

    [Header("Value Variables")]

    public float curHealth;
    public float curMana, curStamina, maxHealth, maxMana, maxStamina, healRate, ManaPerSecond, staminaPerSecond;
    public PlayerStats[] stats;

    [Header("Value Variables")]

    public Image radialHealthIcon;
    public Image radialManaIcon;
    public Image radialStaminaIcon;

    [Header("Damage Effect Variables")]

    public AudioClip deathClip;
    public Image damageImage;
    public Image deathImage;
    public Text text;
    public float flashSpeed = 5;
    public Color flashColor = new Color(1, 0, 0, 0.2f);
    AudioSource playerAudio;
    public static bool isDead;
    bool damaged;
    bool canHeal;
    bool canUseMana;
    bool canUseStamina;
    public float healTimer;
    public float manaTimer;
    public float staminaTimer;

    [Header("Check Point")]
    public Transform curCheckPoint;

    [Header("save")]
    public PlayerSaveAndLoad saveAndLoad;

    [Header("Custom")]
    public bool custom;
    public int skinIndex, eyesIndex, mouthIndex, hairIndex, clothesIndex, armourIndex;
    public CharacterClass charClass;
    public string characterName;
    public string firstCheckPointName = "First Checkpoint";

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!custom)
        {
            HealthChange();
            ManaChange();
            StaminaChange();

            if (Input.GetKeyDown(KeyCode.Z))
            {
                damaged = true;
                curHealth -= 5;
            }
            if (curHealth > 100)
            {
                curHealth = 100; ;
            }
            if (curHealth < 0)
            {
                curHealth = 0; ;
            }
            if (curHealth < maxHealth && curHealth > 0 && canHeal)
            {
                HealOverTime();
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
            if (curHealth <= 0 && !isDead)
            {
                Death();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                curMana -= 5;
            }
            if (curMana > 100)
            {
                curMana = 100;
            }
            if (curMana < 0)
            {
                curMana = 0;
            }
            if (curMana < maxMana && curMana > 0 && canUseMana)
            {
                ManaOverTime();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                curStamina -= 1;
            }
            if (curStamina > 100)
            {
                curStamina = 100;
            }
            if (curStamina < 0)
            {
                curStamina = 0;
            }
            if (curStamina < maxStamina && curStamina > 0 && canUseStamina)
            {
                StaminaOverTime();
            }
        }
    }

    void HealthChange()
    {
        float amount = Mathf.Clamp01(curHealth / maxHealth);
        radialHealthIcon.fillAmount = amount;

        if (curHealth <= 0 && !isDead)
        {
            Death();
        }

        if (!canHeal && curHealth < maxHealth && curHealth > 0)
        {
            healTimer += Time.deltaTime;
            if (healTimer >= 5)
            {
                canHeal = true;
            }
        }
    }
    void ManaChange()
    {
        float amount = Mathf.Clamp01(curMana / maxMana);
        radialManaIcon.fillAmount = amount;

        if (!canUseMana && curMana < maxMana && curMana > 0)
        {
            manaTimer += Time.deltaTime;
            if (manaTimer >= 3)
            {
                canUseMana = true;
            }
        }
    }
    void StaminaChange()
    {
        float amount = Mathf.Clamp01(curStamina / maxStamina);
        radialStaminaIcon.fillAmount = amount;

        if (!canUseStamina && curStamina < maxStamina && curStamina > 0)
        {
            staminaTimer += Time.deltaTime;
            if (staminaTimer >= 3)
            {
                canUseStamina = true;
            }
        }
    }
    void Death()
    {
        isDead = true;
        text.text = "";

        playerAudio.clip = deathClip;
        playerAudio.Play();
        deathImage.gameObject.GetComponent<Animator>().SetTrigger("isDead");
        Invoke("DeathText", 2f);
        Invoke("ReviveText", 6f);
        Invoke("Revive", 9f);

    }
    void Revive()
    {
        text.text = "";
        isDead = false;
        curHealth = maxHealth;
        curMana = maxMana;
        curStamina = maxStamina;

        deathImage.gameObject.GetComponent<Animator>().SetTrigger("Revive");

        this.transform.position = curCheckPoint.position;
        this.transform.rotation = curCheckPoint.rotation;

        deathImage.gameObject.GetComponent<Animator>().SetTrigger("Revive");
    }
    void DeathText()
    {
        text.text = "You 've Fallen in Battle...";
    }
    void ReviveText()
    {
        text.text = "But the Gods have decided it isn't your time...";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            curCheckPoint = other.transform;
            healRate = 5;
            //saveAndLoad.Save();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            healRate = 0;
        }
    }
    public void DamagePlayer(float damage)
    {
        damaged = true;
        curHealth -= damage;
        canHeal = false;
        healTimer = 0;
    }
    public void HealOverTime()
    {
        curHealth += Time.deltaTime * healRate;
    }
    public void ManaOverTime()
    {
        curMana += Time.deltaTime * ManaPerSecond;
    }
    public void StaminaOverTime()
    {
        curStamina += Time.deltaTime * staminaPerSecond;
    }
}