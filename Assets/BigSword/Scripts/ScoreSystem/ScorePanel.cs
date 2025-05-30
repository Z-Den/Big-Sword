using TMPro;
using Units.UI;
using UnityEngine;
using UnityEngine.UI;

namespace BigSword.Scripts.ScoreSystem
{
    public class ScorePanel : UIElement, IUIElementHolder
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Image _progressBar;
        
        private void OnEnable()
        {
            ScoreSystem.Instance.OnScoreChanged += UpdateScore;
            ScoreSystem.Instance.OnNewLevelReached += OnNewLevelReached;
            
            UpdateScore(ScoreSystem.Instance.CurrentScore, ScoreSystem.Instance.NextLevelScore);
            OnNewLevelReached(ScoreSystem.Instance.Level);
        }

        private void OnNewLevelReached(int level)
        {
            _level.text = "Level " + level.ToString();
        }

        private void UpdateScore(int value, int nextLevelScore)
        {
            _value.text = value + " / " + nextLevelScore;
            _progressBar.fillAmount = value / (float)nextLevelScore;
        }

        public UIElement GetUIElement()
        {
            return this;
        }
    }
}