using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class MainMenu : MonoBehaviour
    {
        public static void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public static void QuitGame()
        {
            Application.Quit();
        }
    }
}
