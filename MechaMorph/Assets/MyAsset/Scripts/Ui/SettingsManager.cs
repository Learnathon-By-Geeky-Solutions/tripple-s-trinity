using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using TrippleTrinity.MechaMorph.SaveManager;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown graphicsDropDown;
        [SerializeField] private Slider masterVol, musicVol, sfxVol, lobbyVol;
        [SerializeField] private AudioMixer mainAudioMixer;



        public TMP_Dropdown GraphicsDropDown
        {
            get => graphicsDropDown;
            set => graphicsDropDown = value;
        }

        public void ChangeGraphicsQuality()
        {
            QualitySettings.SetQualityLevel(graphicsDropDown.value, true);
            Debug.Log("Current Graphics Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()]);
            SaveCurrentSettings();
        }

        public void ChangeMasterVol()
        {
            mainAudioMixer.SetFloat("MasterVol", masterVol.value);
            SaveCurrentSettings();
        }
        
        public void ChangeMusicVol()
        {
            mainAudioMixer.SetFloat("MusicVol", musicVol.value);
            SaveCurrentSettings();
        }
        
        public void ChangeSFXVol()
        {
            mainAudioMixer.SetFloat("SFXVol", sfxVol.value);
            SaveCurrentSettings();
        }
        public void ChangeLobbyVol()
        {
            mainAudioMixer.SetFloat("LobbyVol", lobbyVol.value);
            SaveCurrentSettings();
        }

        private void SaveCurrentSettings()
        {
            SettingsData data = new SettingsData
            {
                masterVolume = masterVol.value,
                musicVolume = musicVol.value,
                sfxVolume = sfxVol.value,
                lobbyVolume = lobbyVol.value,
                graphicsQualityIndex = graphicsDropDown.value
            };

            SaveSystem.SaveSettings(data);
        }
    }
}