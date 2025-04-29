using System;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui;
using UnityEngine;
using TrippleTrinity.MechaMorph.Token;
using UnityEngine.SceneManagement;

namespace TrippleTrinity.MechaMorph.SaveManager
{
    public class DeleteData : MonoBehaviour
    {
        private readonly bool  isReset = false;
        private ScoreManager scoreManager;
        public bool IsReset
        {
            get => isReset;
        }

        private void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }

        public static void DeleteEverything()
        {
            SaveSystem.DeleteGame(); // Delete the save.json file
            if (UpgradeManager.Instance != null)
            {
                UpgradeManager.Instance.ResetAllUpgrades(); // Reset PlayerPrefs and in-memory data
                ScoreManager.Instance.ResetScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
            else
            {
                Debug.LogWarning("UpgradeManager instance is null!");
            }
        }
    }
}