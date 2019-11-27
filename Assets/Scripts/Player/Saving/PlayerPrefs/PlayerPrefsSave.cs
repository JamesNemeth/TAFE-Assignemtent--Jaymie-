using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//General Game Settings 
public class PlayerPrefsSave : MonoBehaviour
{
    public PlayerHandler player;
    public float x, y, z;
    public float rotx, roty, rotz, rotw;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Loaded"))
        {
            PlayerPrefs.DeleteAll();
            Load(player);
            PlayerPrefs.SetInt("Loaded", 0);
            //Save Binary Data
        }
        else
        {
            //Load Binary 
        }
    }


    public void Save(PlayerHandler player)
    {
        //Health, Mana, Stamina
        //PLayerPrefs save Float with key CurHealth and the players current hralth value
        PlayerPrefs.SetFloat("CurHealth", player.curHealth);
        PlayerPrefs.SetFloat("CurMana", player.curMana);
        PlayerPrefs.SetFloat("CurStamina", player.curStamina);
        //Position 
        PlayerPrefs.SetFloat("PosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PosZ", player.transform.position.z);
        //Rotation
        PlayerPrefs.SetFloat("RotX", player.transform.rotation.x);
        PlayerPrefs.SetFloat("RotY", player.transform.rotation.y);
        PlayerPrefs.SetFloat("RotZ", player.transform.rotation.z);
        PlayerPrefs.SetFloat("RotW", player.transform.rotation.w);
    }

    public void Load(PlayerHandler player)
    {
        //Health, Mana, Stamina
        //PLayers current values is set to PlayerPrefs saved float called CurHealth, else set to MaxHealth
        player.curHealth = PlayerPrefs.GetFloat("CurHealth", player.maxHealth);
        player.curMana = PlayerPrefs.GetFloat("CurMana", player.maxMana);
        player.curStamina = PlayerPrefs.GetFloat("CurStamina", player.maxStamina);
        //Position 
        x = PlayerPrefs.GetFloat("PosX", 1);
        y = PlayerPrefs.GetFloat("PosY", 1);
        z = PlayerPrefs.GetFloat("PosZ", 1);
        player.transform.position = new Vector3(x, y, z);
        //Rotation
        rotx = PlayerPrefs.GetFloat("RotX", 0);
        roty = PlayerPrefs.GetFloat("RotY", 0);
        rotz = PlayerPrefs.GetFloat("RotZ", 0);
        rotw = PlayerPrefs.GetFloat("RotW", 0);
        player.transform.rotation = new Quaternion(rotx, roty, rotz, rotw);
    }
}

