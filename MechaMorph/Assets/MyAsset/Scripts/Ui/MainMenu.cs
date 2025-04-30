using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using TrippleTrinity.MechaMorph.SaveManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class MainMenu : MonoBehaviour 
    {
        public static void PlayGame()
        {
            SettingsData settingsData = SaveSystem.LoadSettings();

            switch (settingsData.gameMode)
            {
                case DifficultyMode.Easy:
                    SceneManager.LoadScene("EasyModeScene");
                    break;
                case DifficultyMode.Medium:
                    SceneManager.LoadScene("MediumModeScene");
                    break;
                case DifficultyMode.Hard:
                    SceneManager.LoadScene("HardModeScene");
                    break;
                default:
                    SceneManager.LoadScene("EasyModeScene");
                    break;
            }
        }


        public void QuitGame() // ✅ Non-static
        {
            Application.Quit();
        }
    }
}