namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager
{
    [System.Serializable]
    public class GameData
    {
        public int score;
        public int highScore;
        public int tokenCount;
        public int areaDamageLevel;
        public int boosterCooldownLevel;

        public float masterVol, musicVol, sfxVol, lobbyVol;
        public int graphicsQuality;

        public string gameMode;

        public int totalTokens; // Add this field to store total tokens

        // Constructor with default values
        public GameData(int score = 0, int highScore = 0, int tokenCount = 0, int areaDamageLevel = 0, 
            int boosterCooldownLevel = 0, float masterVol = 1.0f, float musicVol = 1.0f, 
            float sfxVol = 1.0f, float lobbyVol = 1.0f, int graphicsQuality = 1, string gameMode = "Easy", int totalTokens = 0)
        {
            this.score = score;
            this.highScore = highScore;
            this.tokenCount = tokenCount;
            this.areaDamageLevel = areaDamageLevel;
            this.boosterCooldownLevel = boosterCooldownLevel;
            this.masterVol = masterVol;
            this.musicVol = musicVol;
            this.sfxVol = sfxVol;
            this.lobbyVol = lobbyVol;
            this.graphicsQuality = graphicsQuality;
            this.gameMode = gameMode;
            this.totalTokens = totalTokens; // Initialize the totalTokens
        }
    }
}