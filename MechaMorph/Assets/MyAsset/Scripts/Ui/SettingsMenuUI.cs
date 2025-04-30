using TMPro;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using TrippleTrinity.MechaMorph.SaveManager;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class SettingsMenuUI : MonoBehaviour
    {
        public TMP_Dropdown difficultyDropdown; // Assign in Inspector

        private void Start()
        {
            LoadSettings(); // Load when menu opens
        }

        public void LoadSettings()
        {
            SettingsData settingsData = SaveSystem.LoadSettings();
            if (settingsData != null)
            {
                difficultyDropdown.value = (int)settingsData.gameMode;
                difficultyDropdown.RefreshShownValue(); // Important to update label
            }
        }

        public void OnApplySettings()
        {
            SettingsData settingsData = new SettingsData();

            settingsData.gameMode = (DifficultyMode)difficultyDropdown.value;

            SaveSystem.SaveSettings(settingsData);
        }
    }
}