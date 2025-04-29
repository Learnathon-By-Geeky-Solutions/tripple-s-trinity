using TMPro;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class DifficultyDropdownHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown levelDropdown;

        private void Start()
        {
            // Set dropdown default (optional)
            levelDropdown.onValueChanged.AddListener(OnDropdownChanged);
        }

        private void OnDropdownChanged(int index)
        {
            switch (index)
            {
                case 0:
                    GameModeManager.Instance.SetMode("Easy");
                    break;
                case 1:
                    GameModeManager.Instance.SetMode("Medium");
                    break;
                case 2:
                    GameModeManager.Instance.SetMode("Hard");
                    break;
            }
        }
    }
}