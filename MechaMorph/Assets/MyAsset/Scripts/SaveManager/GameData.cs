using System;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager
{
    [Serializable]
    public class GameData
    {
        public int score;
        public int tokenCount;
        public int highScore;

        public int boosterUpgradeLevel;
        public int areaDamageUpgradeLevel;
    }
}