using System;
using Bootstrapper;
using Units.Health;
using Units.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _deadPanel;
        [SerializeField] private PlayerBootstrapper _playerBootstrapper;

        private void Start()
        {
            _deadPanel.SetActive(false);
            _playerBootstrapper.Player.GetComponent<UnitHealth>().OnDeath += OnDeath;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDeath()
        {
            _deadPanel.SetActive(true);
        }
    }
}
