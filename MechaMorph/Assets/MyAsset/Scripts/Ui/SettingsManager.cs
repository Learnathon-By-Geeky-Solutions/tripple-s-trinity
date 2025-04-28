using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

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
        }

        public void ChangeMasterVol()
        {
            mainAudioMixer.SetFloat("MasterVol", masterVol.value);
        }
        
        public void ChangeMusicVol()
        {
            mainAudioMixer.SetFloat("MusicVol", musicVol.value);
        }
        
        public void ChangeSFXVol()
        {
            mainAudioMixer.SetFloat("SFXVol", sfxVol.value);
        }
        public void ChangeLobbyVol()
        {
            mainAudioMixer.SetFloat("LobbyVol", lobbyVol.value);
        }
    }
}