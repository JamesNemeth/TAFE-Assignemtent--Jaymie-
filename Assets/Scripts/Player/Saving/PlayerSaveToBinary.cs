using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerSaveToBinary
{
    public static void SavePlayerData(PlayerHandler player)
    {
        //Reference a Binary Formatter
        BinaryFormatter formatter = new BinaryFormatter();
        //Location to Save
        string path = Application.persistentDataPath + "/" + PlayerDataToSave.saveSlot;
        //Create a file path
        FileStream stream = new FileStream(path, FileMode.Create);
        //What Data to write to the file
        PlayerDataToSave data = new PlayerDataToSave(player);
        //write it and convert to bytes for writing to binary
        formatter.Serialize(stream, data);
        //Ending
        stream.Close();
    }
    public static PlayerDataToSave LoadData(PlayerHandler player)
    {
        //Location to Save
        string path = Application.persistentDataPath + "/" + PlayerDataToSave.saveSlot;
        //If we have the file at that path
        if (File.Exists(path))
        {
            //get our Binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            //and reat the data
            FileStream stream = new FileStream(path, FileMode.Open);
            //set the data from what it is back to usable variables
            PlayerDataToSave data = formatter.Deserialize(stream) as PlayerDataToSave;
            //Ending
            stream.Close();
            //Send usable data back to the PLayerDataSave Script
            return data;
        }
        else
        {
            return null;
        }
    }
}
