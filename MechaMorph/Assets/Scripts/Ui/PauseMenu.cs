using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        private bool _isPaused;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        private void TogglePause()
        {
            _isPaused = !_isPaused;
            pauseMenu.SetActive(_isPaused);
            Time.timeScale = _isPaused ? 0 : 1;
        }

        public void Resume()
        {
            _isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        public static void Home()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("StartManu"); // Fixed potential typo
        }

        public static void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}