using UnityEngine;
using TMPro;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Dropdown graphicsDropDown;

        public TMP_Dropdown GraphicsDropDown
        {
            get => graphicsDropDown;
            set => graphicsDropDown = value;
        }

        public void ChangeGraphicsQuality()
        {
            QualitySettings.SetQualityLevel(graphicsDropDown.value, true);
            Debug.Log("Current Graphics Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()]);
        }
    }
}