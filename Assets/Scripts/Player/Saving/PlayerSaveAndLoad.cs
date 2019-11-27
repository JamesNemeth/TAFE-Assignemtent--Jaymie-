using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveAndLoad : MonoBehaviour
{
    public PlayerHandler player;
    public bool custom;

    private void Awake()
    {
        if (!custom)
        {
            //Load Binary
            Load();
        }
    }
    public void Save()
    {
        PlayerSaveToBinary.SavePlayerData(player);
    }

    public void Load()
    {
        PlayerDataToSave data = PlayerSaveToBinary.LoadData(player);
        player.name = data.playerName;
        player.curCheckPoint = GameObject.Find(data.checkPoint).GetComponent<Transform>();
        player.maxHealth = data.maxHealth;
        player.maxMana = data.maxMana;
        player.maxStamina = data.maxStamina;

        player.curHealth = data.curHealth;
        player.curMana = data.curMana;
        player.curStamina = data.curStamina;

        if (!(data.posx == 0 && data.posy == 0 && data.posz == 0))
        {
            player.transform.position = new Vector3(data.posx, data.posy, data.posz);
            player.transform.rotation = new Quaternion(data.rotx, data.roty, data.rotz, data.rotw);
        }
        else
        {
            player.transform.position = player.curCheckPoint.position;
            player.transform.rotation = player.curCheckPoint.rotation;
        }

        player.skinIndex = data.skinIndex;
        player.eyesIndex = data.eyesIndex;
        player.mouthIndex = data.mouthIndex;
        player.hairIndex = data.hairIndex;
        player.clothesIndex = data.clothesIndex;
        player.armourIndex = data.armourIndex;

        player.charClass = (CharacterClass)data.classIndex;
        player.characterName = data.playerName;

        for (int i = 0; i < player.stats.Length; i++)
        {
            player.stats[i].value = data.stats[i];
        }
    }
}
