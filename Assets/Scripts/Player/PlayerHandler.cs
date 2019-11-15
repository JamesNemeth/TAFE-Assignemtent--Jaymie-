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
    public float curMana, curStamina, maxHealth, maxMana, maxStamina, healRate;
    public PlayerStats[] stats;

    [Header("Value Variables")]

    public Slider healthBar;
    public Slider manaBar;
    public Slider staminaBar;
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
    float healTimer;

    [Header("Check Point")]
    public Transform curCheckPoint;

    [Header("save")]
    //public PlayerSaveAndLoad saveAndLoad;

    [Header("Custom")]
    public bool custom;
    public int skinIndex, eyesIndex, mouthIndex, hairIndex, clothesIndex, armourIndex;
   // public CharacterClass charClass;
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
            if (healthBar.value != Mathf.Clamp01(curHealth / maxHealth))
            {
                curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
                healthBar.value = Mathf.Clamp01(curHealth / maxHealth);
            }
            if (manaBar.value != Mathf.Clamp01(curMana / maxMana))
            {
                curMana = Mathf.Clamp(curMana, 0, maxMana);
                manaBar.value = Mathf.Clamp01(curMana / maxMana);
            }
            if (staminaBar.value != Mathf.Clamp01(curStamina / maxStamina))
            {
                curStamina = Mathf.Clamp(curStamina, 0, maxStamina);
                staminaBar.value = Mathf.Clamp01(curStamina / maxStamina);
            }
            if (curHealth <= 0 && !isDead)
            {
                Death();
            }

            HealthChange();
            ManaChange();
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            damaged = true;
            curHealth -= 5;
        }
        if (damaged && !isDead)
        {
            damageImage.color = flashColor;
            damaged = false;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
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
    }
    private void LateUpdate()
    {
        if (curHealth < maxHealth && curHealth > 0 && canHeal)
        {
            HealOverTime();
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
        curHealth += Time.deltaTime * (healRate);
    }
}