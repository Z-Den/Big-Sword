using Bootstrapper;
using SaveLoadSystem;
using Units.Health;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        private GameObject _deadPanelPrefab;
        private SaveData _currentSave;
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        public void Init(GameObject deadPanelPrefab)
        {
            _deadPanelPrefab = deadPanelPrefab;
            _instance = FindAnyObjectByType<GameManager>();
            _deadPanelPrefab.SetActive(false);
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
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
            var canvas = FindAnyObjectByType<Canvas>(FindObjectsInactive.Exclude);
            var deadPanel = Instantiate(_deadPanelPrefab, canvas.transform);
            _deadPanelPrefab.SetActive(true);
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
                Debug.Log(_currentSave.PlayerPositionByVector);
                playerBootstrapper.Init(_currentSave.PlayerPositionByVector);
            }
        }
    }
}
