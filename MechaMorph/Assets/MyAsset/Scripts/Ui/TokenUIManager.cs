using TMPro;
using UnityEngine;
using System.Collections;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class TokenUIManager : MonoBehaviour
    {
        public static TokenUIManager Instance { get; private set; }

        [SerializeField] private TMP_Text tokenCountText;

        private int _currentGameTokens;
        private int _totalTokens;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadTotalTokens();
                _currentGameTokens = 0;
                UpdateTokenCount(_currentGameTokens);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdateTokenCount(int count)
        {
            _currentGameTokens = count;
            if (tokenCountText != null)
            {
                tokenCountText.text = $"<color=#004DFF>Tokens: {_currentGameTokens:D2}</color>";
                StopAllCoroutines();
                StartCoroutine(AnimateTokenText());
            }
        }

        public void ResetTokenCount()
        {
            _currentGameTokens = 0;
            UpdateTokenCount(0);
        }

        public void AddToken()
        {
            UpdateTokenCount(_currentGameTokens + 1);
        }

        public void SaveFinalTokenCount()
        {
            _totalTokens += _currentGameTokens;

            GameData currentSave = SaveSystem.LoadGame() ?? new GameData();
            currentSave.tokenCount = _totalTokens;
            currentSave.highScore = ScoreManager.Instance.CurrentScore;

            SaveSystem.SaveGame();

            Debug.Log($"Game Over! Total Tokens Saved: {_totalTokens}");
            ResetTokenCount();
        }

        public int CurrentTokenCount() => _currentGameTokens;
        public int GetTotalTokens() => _totalTokens;

        private void LoadTotalTokens()
        {
            GameData data = SaveSystem.LoadGame();
            _totalTokens = data?.tokenCount ?? 0;
        }

        private IEnumerator AnimateTokenText()
        {
            Vector3 originalScale = tokenCountText.transform.localScale;
            tokenCountText.transform.localScale = originalScale * 1.2f;
            yield return new WaitForSeconds(0.1f);
            tokenCountText.transform.localScale = originalScale;
        }

        // 🔧 Fix: Add LoadTokenCount method
        public void LoadTokenCount(int count)
        {
            _currentGameTokens = count;
            UpdateTokenCount(count);
        }
        
    }
}