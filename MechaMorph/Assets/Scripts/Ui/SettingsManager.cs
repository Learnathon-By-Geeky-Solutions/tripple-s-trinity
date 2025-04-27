using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class SettingsManager : MonoBehaviour
    {
        public TMP_Dropdown graphicsDropDown;

        public void ChangeGraphicsQuality()
        {
            QualitySettings.SetQualityLevel(graphicsDropDown.value, true);
            Debug.Log("Current Graphics Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()]);

        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
