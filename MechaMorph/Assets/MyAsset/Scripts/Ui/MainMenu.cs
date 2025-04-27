using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class MainMenu : MonoBehaviour
    {
        public static void PlayGame()
        {
            SceneManager.LoadScene("MainScene");
        }

        public static void QuitGame()
        {
            Application.Quit();
        }
    }
}
