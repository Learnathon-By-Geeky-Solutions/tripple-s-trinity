using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager
{
    public static class SettingsData
    {
        public static float MasterVol;
        public static float MusicVol;
        public static float SfxVol;
        public static float LobbyVol;

        public static void LoadFromGameData(GameData data)
        {
            MasterVol = data.masterVol;
            MusicVol = data.musicVol;
            SfxVol = data.sfxVol;
            LobbyVol = data.lobbyVol;
            QualitySettings.SetQualityLevel(data.graphicsQuality);
        }
    }
}