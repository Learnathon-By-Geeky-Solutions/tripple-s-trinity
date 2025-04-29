using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using UnityEngine;
using UnityEngine.SceneManagement;

// Assuming GameModeManager is in this namespace

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class StartMenu : MonoBehaviour
    {
        public void StartEasy()
        {
            GameModeManager.Instance.SetMode("Easy");
            SceneManager.LoadScene("EasyScene"); // Make sure "EasyScene" is in your Build Settings
        }

        public void StartMedium()
        {
            GameModeManager.Instance.SetMode("Medium");
            SceneManager.LoadScene("MediumScene"); // Make sure "MediumScene" is in your Build Settings
        }

        public void StartHard()
        {
            GameModeManager.Instance.SetMode("Hard");
            SceneManager.LoadScene("HardScene"); // Make sure "HardScene" is in your Build Settings
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}