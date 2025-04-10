using System;
using UnityEngine.SocialPlatforms.Impl;

namespace TrippleTrinity.MechaMorph.SaveManager

{
    [Serializable]
    public class GameData
    {
        public int score=0;
        public int highScore = 0;
        public int tokenCount=0;
        public int boosterUpgradeLevel = 0;
        public int areaDamageUpgradeLevel = 0;
        public int boosterUpgradeCost = 5;
        public int areaDamageUpgradeCost = 5;
    }
}
