using System.Collections.Generic;
using System.Linq;
using PivotConnection;
using SaveLoadSystem;
using Units;
using Units.Input;
using Units.Player;
using Units.UI;
using UnityEngine;

namespace Bootstrapper
{
    public class PlayerBootstrapper : MonoBehaviour
    {
        [SerializeField] private string _saveFileName;
        [SerializeField] private Transform _playerSpawnPoint;
        [Header("Prefabs")]
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private UnitUI _playerUIPrefab;
        [SerializeField] private List<GameObject> _playerItemPrefabs;
        private Player _player;
        private UnitUI _playerUI;
        
        public Player Player => _player;
        
        private void Awake()
        {
            _player = InstantiatePrefab(_playerPrefab);
            _playerUI = InstantiatePrefab(_playerUIPrefab);
            
            var items = new List<GameObject>();
            items.Add(_player.gameObject);
            items.AddRange(_playerItemPrefabs.Select(InstantiatePrefab).ToList());
            
            CreateSaveLoadService();
            
            ConfigureDependencies(items);
        }

        private T InstantiatePrefab<T>(T prefab) where T : Object
        {
            return Instantiate(prefab, _playerSpawnPoint.position, Quaternion.identity);
        }

        private void CreateSaveLoadService()
        {
            var jsonSaveLoadService = new JsonSaveLoadRepository();
            var obj = new GameObject("SaveLoadService");
            var service = obj.AddComponent<SaveLoadService>();
            service.Init(_player, jsonSaveLoadService);
            DontDestroyOnLoad(obj);
        }
        
        private void ConfigureDependencies(List<GameObject> connectedObjects)
        {
            var input = new PlayerInput();
            _player.gameObject.TryGetComponent(out IPivot pivot);
            
            foreach (var connectedObject in connectedObjects)
            {
                foreach (var component in connectedObject.GetComponents<IUnitActionController>())
                {
                    component.SetInput(input);
                }

                foreach (var component in connectedObject.GetComponents<IPivotFollower>())
                {
                    component.SetPivot(pivot);
                }

                foreach (var component in connectedObject.GetComponents<IUIElementHolder>())
                {
                    var element = component.GetUIElement();
                    if (element != null)
                        _playerUI.Add(element);
                }
            }
        }
    }
}