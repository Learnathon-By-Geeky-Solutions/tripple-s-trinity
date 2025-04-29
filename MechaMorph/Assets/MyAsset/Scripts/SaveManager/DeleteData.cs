using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using UpgradeManager = TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui.UpgradeManager;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager
{
    public class DeleteData : MonoBehaviour
    {
        private readonly bool  _isReset = false;
        private ScoreManager scoreManager;
        public bool IsReset
        {
            get => _isReset;
        }

        private void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }

        public static void DeleteEverything()
        {
            SaveSystem.DeleteGame(); // Delete save.json

            if (UpgradeManager.Instance != null)
            {
                UpgradeManager.Instance.ResetAllUpgrades(); // 

                if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.ResetScore();
                }
                else
                {
                    Debug.LogWarning("ScoreManager instance is null when trying to reset score.");
                }

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Debug.LogWarning("UpgradeManager instance is null!");
            }
        }

    }
}