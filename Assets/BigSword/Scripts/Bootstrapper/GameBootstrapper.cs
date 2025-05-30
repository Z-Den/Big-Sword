using System.Collections.Generic;
using BigSword.Scripts.ScoreSystem;
using SaveLoadSystem;
using SpawnerSystem;
using UI.PauseMenu;
using Units.Input;
using UnityEngine;

namespace Bootstrapper
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameObject _deadPanelPrefab;
        [SerializeField] private GameObject _completePrefab;
        [SerializeField] private PauseMenuController _pauseMenuPrefab;
        [SerializeField] private Canvas _canvas;
        [Header("Spawner")]
        [SerializeField] private SpawnerType _spawnerType;
        [SerializeField] private List<MobSpawnData> _spawnData;
        private PauseMenuController _pauseMenu;
        
        private void Awake()
        {
            _pauseMenu = Instantiate(_pauseMenuPrefab, _canvas.transform);
            ((IUnitActionController)_pauseMenu).SetInput(new PlayerInput());
            
            CreateSaveLoadService();
            CreateGameManagerService();
            CreateScoreService();
            CreateMobSpawner();
        }

        private void CreateSaveLoadService()
        {
            var jsonSaveLoadService = new JsonSaveLoadRepository();
            var obj = new GameObject("SaveLoadService");
            var service = obj.AddComponent<SaveLoadService>();
            service.Init(jsonSaveLoadService);
            DontDestroyOnLoad(obj);
        }
        
        private void CreateGameManagerService()
        {
            var obj = new GameObject("GameManager");
            var manager = obj.AddComponent<GameManager.GameManager>();
            manager.Init(_deadPanelPrefab, _completePrefab);
            DontDestroyOnLoad(obj);
        }
        
        private void CreateScoreService()
        {
            var obj = new GameObject("ScoreManager");
            var manager = obj.AddComponent<ScoreSystem>();
            manager.Init();
            DontDestroyOnLoad(obj);
        }
        
        private void CreateMobSpawner()
        {
            var obj = new GameObject("MobSpawner");
            var manager = obj.AddComponent<MobSpawner>();
            manager.Init(_spawnerType, _spawnData);
            DontDestroyOnLoad(obj);
        }
    }
}