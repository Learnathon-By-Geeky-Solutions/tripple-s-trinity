using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager
{
    public class GameModeManager : MonoBehaviour
    {
        public static GameModeManager Instance { get; private set; }

        public string CurrentMode { get; private set; } = "Medium"; // default

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetMode(string mode)
        {
            CurrentMode = mode;
        }
    }
}