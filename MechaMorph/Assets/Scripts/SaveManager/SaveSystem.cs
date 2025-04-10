using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
namespace TrippleTrinity.MechaMorph.SaveManager
{
    public static class SaveSystem 
    {
       private static string _savePath= Application.persistentDataPath+ "/save.json";

       public static void SaveGame(GameData data)
       {
           
           string json = JsonUtility.ToJson(data);
           File.WriteAllText(_savePath, json);
           Debug.Log("Game Saved to: "+ _savePath);
       }

       public static GameData LoadGame()
       {
           if (File.Exists(_savePath))
           {
               string json = File.ReadAllText(_savePath);
                return JsonUtility.FromJson<GameData>(json);
                /*GameData data = JsonUtility.FromJson<GameData>(json);
                Debug.Log("Game Loaded from: "+ _savePath);
                return data;*/
            }
           else
           {
               //Debug.LogWarning("save file not found");
               return null;
           }
       }

       public static void DeleteGame()
       {
           if (File.Exists(_savePath))
           {
               File.Delete(_savePath);
               Debug.Log("Game Deleted from: "+ _savePath);
           }
       }
    }
}
