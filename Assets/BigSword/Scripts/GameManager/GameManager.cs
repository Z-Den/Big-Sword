using System;
using System.Linq;
using BigSword.Scripts.ScoreSystem;
using Bootstrapper;
using SaveLoadSystem;
using Units.Health;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        private int _levelToCompleteGame = 10;
        private GameObject _deadPanelPrefab;
        private GameObject _completeGamePrefab;
        private SaveData _currentSave;
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        public void Init(GameObject deadPanelPrefab, GameObject completeGamePrefab)
        {
            _deadPanelPrefab = deadPanelPrefab;
            _completeGamePrefab = completeGamePrefab;
            _instance = FindAnyObjectByType<GameManager>();
            _deadPanelPrefab.SetActive(false);
            _completeGamePrefab.SetActive(false);
        }

        private void Start()
        {
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
            ScoreSystem.Instance.OnNewLevelReached += OnNewLevelReached;
        }

        private void OnNewLevelReached(int level)
        {
            if (_levelToCompleteGame == level)
            {
                var completeGamePrefab = Instantiate(_completeGamePrefab, GetCanvas().transform);
                completeGamePrefab.SetActive(true);
            }
        }

        public void StartNewGame()
        {
            _currentSave = SaveLoadService.Instance.GetNewSaveData();
            SceneManager.LoadScene(_currentSave.SceneIndex);
        }
        
        public void LoadGame(string loadFileName)
        {
            _currentSave = SaveLoadService.Instance.GetPlayerData(loadFileName);
            SceneManager.LoadScene(_currentSave.SceneIndex);
        }

        public void ShowDeadPanel()
        {
            var deadPanel = Instantiate(_deadPanelPrefab, GetCanvas().transform);
            deadPanel.SetActive(true);
        }

        private Canvas GetCanvas()
        {
            var canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            return canvases.FirstOrDefault(canvas => canvas.renderMode == RenderMode.ScreenSpaceOverlay);
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            var playerBootstrapper = FindAnyObjectByType<PlayerBootstrapper>();
            if (playerBootstrapper != null)
            {
                playerBootstrapper.Init(_currentSave.PlayerPositionByVector);
            }
        }
    }
}
