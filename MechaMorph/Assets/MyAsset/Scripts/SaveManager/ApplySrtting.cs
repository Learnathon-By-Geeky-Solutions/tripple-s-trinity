using TrippleTrinity.MechaMorph.SaveManager;
using UnityEngine;
using UnityEngine.Audio;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager
{
    public class ApplySettingsOnStart : MonoBehaviour
    {
        [SerializeField] private AudioMixer mainMixer;

        void Start()
        {
            SettingsData settings = SaveSystem.LoadSettings();

            mainMixer.SetFloat("MasterVol", settings.masterVolume);
            mainMixer.SetFloat("MusicVol", settings.musicVolume);
            mainMixer.SetFloat("SFXVol", settings.sfxVolume);
            mainMixer.SetFloat("LobbyVol", settings.lobbyVolume);

            QualitySettings.SetQualityLevel(settings.graphicsQualityIndex, true);
        }
    }
}