using TMPro;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using TrippleTrinity.MechaMorph.SaveManager;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class SettingsMenuUI : MonoBehaviour
    {
        public TMP_Dropdown difficultyDropdown; // Attach TMP Dropdown in Inspector

        public void OnApplySettings()
        {
            SettingsData settingsData = new SettingsData();

            settingsData.gameMode = (DifficultyMode)difficultyDropdown.value;

            // Optional: add other settings like volume, quality, etc.

            SaveSystem.SaveSettings(settingsData);
        }
    }
}