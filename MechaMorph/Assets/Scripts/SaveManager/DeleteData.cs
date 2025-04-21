using UnityEngine;
using TrippleTrinity.MechaMorph.Token;
using UnityEngine.SceneManagement;

namespace TrippleTrinity.MechaMorph.SaveManager
{
    public class DeleteData : MonoBehaviour
    {
        private bool isReset = false;

        public bool IsReset
        {
            get => isReset;
        }

        public void DeleteEverything()
        {
            SaveSystem.DeleteGame(); // Delete the save.json file
            if (UpgradeManager.Instance != null)
            {
                UpgradeManager.Instance.ResetAllUpgrades(); // Reset PlayerPrefs and in-memory data
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
            else
            {
                Debug.LogWarning("UpgradeManager instance is null!");
            }
        }
    }
}