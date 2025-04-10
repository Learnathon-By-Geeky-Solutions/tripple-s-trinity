using UnityEngine;
using TrippleTrinity.MechaMorph.Ui;
using TrippleTrinity.MechaMorph.Token;
namespace TrippleTrinity.MechaMorph.SaveManager
{
    public class LoadGameData : MonoBehaviour
    {
        private void Start()
        {
            GameData data = SaveSystem.LoadGame();

            // Initialize managers
            var upgradeManager = UpgradeManager.Instance;
            var scoreManager = ScoreManager.Instance;

            
        }
        
    }
}
