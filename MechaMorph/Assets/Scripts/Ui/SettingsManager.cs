using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class SettingsManager : MonoBehaviour
    {
        public TMP_Dropdown graphichDropDown;

        public void ChangeGraphicsQuality()
        {
            QualitySettings.SetQualityLevel(graphichDropDown.value);
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
