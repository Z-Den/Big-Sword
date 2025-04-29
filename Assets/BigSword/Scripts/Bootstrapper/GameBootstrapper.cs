using System;
using SaveLoadSystem;
using UI.PauseMenu;
using Units.Input;
using Units.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Bootstrapper
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameObject _deadPanelPrefab;
        [SerializeField] private PauseMenuController _pauseMenuPrefab;
        [SerializeField] private Canvas _canvas;
        private PauseMenuController _pauseMenu;
        
        private void Awake()
        {
            _pauseMenu = Instantiate(_pauseMenuPrefab, _canvas.transform);
            ((IUnitActionController)_pauseMenu).SetInput(new PlayerInput());
            
            CreateSaveLoadService();
            CreateGameManagerService();
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
            manager.Init(_deadPanelPrefab);
            DontDestroyOnLoad(obj);
        }
    }
}