using System;
using System.Collections.Generic;
using SpawnerSystem;
using Units;
using Units.Enemy;
using UnityEngine;

namespace BigSword.Scripts.ScoreSystem
{
    public class ScoreSystem : MonoBehaviour
    {
        private int _level = 0;
        private int _score = 0;
        private int _scoreForLevel1 = 100;
        private int _additionScorePerLevel = 150;
        private int _nextLevelScore;
            
        private static ScoreSystem _instance;
        public static ScoreSystem Instance => _instance;
        
        public int CurrentScore => _score;
        public int NextLevelScore => _nextLevelScore;
        public int Level => _level;
        
        public Action<int, int> OnScoreChanged;
        public Action<int> OnNewLevelReached;

        public void Init()
        {
            _instance = FindAnyObjectByType<ScoreSystem>();
            _nextLevelScore = _scoreForLevel1;
        }

        private void Start()
        {
            MobSpawner.Instance.OnSpawn += OnSpawn;
            OnScoreChanged?.Invoke(_score, _nextLevelScore);
            OnNewLevelReached?.Invoke(_level);
        }

        private void OnSpawn(Enemy unit)
        {
            unit.Health.OnDeath += () => AddScore(unit.ScoreToKill);
        }

        private void AddScore(int additionScore)
        {
            _score += additionScore;
            if (IsScoreEnough(_score))
            {
                _score -= _nextLevelScore;
                _nextLevelScore = _scoreForLevel1 + ++_level * _additionScorePerLevel;
                OnNewLevelReached?.Invoke(_level);
            }
            OnScoreChanged?.Invoke(_score, _nextLevelScore);
        }

        private bool IsScoreEnough(int score)
        {
            return score >= _nextLevelScore;
        }
    }
}