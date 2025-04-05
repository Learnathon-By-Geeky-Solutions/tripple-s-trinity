
using UnityEngine;
using TrippleTrinity.MechaMorph.Ui;
namespace TrippleTrinity.MechaMorph.SaveManager
{
    public class LoadGameData : MonoBehaviour
    {
        private void Start()
        {
            GameData data = SaveSystem.LoadGame();
            if (data != null)
            {
                //ScoreManager.Instance.LoadScore(data.score);
               //TokenUIManager.Instance.LoadTokenCount(data.tokenCount);
            }
            else
            {
                Debug.Log("No saved data found. Starting new game.");
            }
        }
        
    }
}
