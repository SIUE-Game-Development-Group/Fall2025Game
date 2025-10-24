using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using Core.Scripts.Game;

public class SaveLoadManager : MonoBehaviour
{
    /*
        TODO:
            - On load, make sure to replace current weapon player is holding using
            SwapItem function in ItemManager script!`
    */

    string path;
    
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        if (path == null) path = Application.persistentDataPath + "/inventorySave.dat";
        
        FileStream stream = new FileStream(path, FileMode.Create);

        if (ItemManager.inventory == null)
        {
            Debug.LogError("SaveLoadManager: Could not save because ItemManager.inventory is null!");
            return;
        }
        
        formatter.Serialize(stream, ItemManager.inventory);
        stream.Close();
        Debug.Log("Game Saved!");
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ItemManager.inventory = formatter.Deserialize(stream) as List<Item>;

            // Swap current weapons here!!
            ItemManager manager = new ItemManager();
            manager.SwapItem(ItemManager.inventory[0].name);
            
            stream.Close();
            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }
}
