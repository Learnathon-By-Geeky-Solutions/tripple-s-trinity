using System;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager
{
    [Serializable]
    public class SettingsData
    {
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
        public float lobbyVolume;
        public int graphicsQualityIndex;

        public DifficultyMode gameMode = DifficultyMode.Easy; 
    }
}