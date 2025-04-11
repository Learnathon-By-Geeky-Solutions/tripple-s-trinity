using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;
    public int tokenCount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ResetGame()
    {
        score = 0;
        tokenCount = 0;
        Debug.Log("Game values reset.");
    }
}