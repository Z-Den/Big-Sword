using SaveLoadSystem;
using Units.Input;
using UnityEngine;

namespace UI.PauseMenu
{
    public class PauseMenuModel
    {
        private bool _isOpen = true;
        private PlayerInput _input;
        
        public bool IsOpen => _isOpen;

        public PauseMenuModel(PlayerInput input)
        {
            _input = input;
        }
        
        public void SetMenuActive(bool isActive)
        {
            _isOpen = isActive;
            _input.SetInputScheme(_isOpen? PlayerInputScheme.UI : PlayerInputScheme.Battle);
            Time.timeScale = isActive ? 0 : 1;
        }

        public void StartNewGame()
        {
            GameManager.GameManager.Instance.StartNewGame();
        }

        public void LoadLatestSave()
        {
            var saveData = SaveLoadService.Instance.GetLatestSaveData();
            GameManager.GameManager.Instance.LoadGame(saveData.SaveName);
        }
    }
}