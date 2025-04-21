using UI.PauseMenu.SaveLoad;
using UI.PauseMenu.Settings;
using Units.Input;
using UnityEditor;
using UnityEngine;

namespace UI.PauseMenu
{
    public class PauseMenuController : MonoBehaviour, IUnitActionController
    {
        private PauseMenuModel _model;
        private PauseMenuView _view;
        private IUnitInput _inputActions;
        private MenuPanel _openMenuPanel;


        private void Start()
        {
            _model = new PauseMenuModel(_inputActions as PlayerInput);
            _view = GetComponent<PauseMenuView>();

            _inputActions.MenuButtonPressed += OpenMenu;
            _inputActions.UIBackButtonPressed += BackKeyPressed;
            _inputActions.UIApplyButtonPressed += ApplyKeyPressed;

            CloseMenu();
        }

        private void ApplyKeyPressed()
        {

        }

        private void BackKeyPressed()
        {
            if (_openMenuPanel != null)
                _openMenuPanel.BackButtonPressed(ClosePanel);
            else
                CloseMenu();
        }

        private void ClosePanel()
        {
            _view.ClosePanel();
            _openMenuPanel = null;
        }

        private void OpenMenu()
        {
            if (_model.IsOpen) return;

            _model.SetMenuActive(true);
            _view.SetActive(true);
        }

        private void CloseMenu()
        {
            if (!_model.IsOpen) return;

            _model.SetMenuActive(false);
            _view.SetActive(false);
        }

        public void OnLastSaveDataLoaded()
        {
            _model.LoadLatestSave();
        }

        public void OnNewGameButtonPressed()
        {
            _model.StartNewGame();
        }

    public void OnContinueButtonClicked()
        {
            CloseMenu();
        }

        public void OnPanelButtonClicked(MenuPanel panel)
        {
            _view.ShowPanel(panel);
            _openMenuPanel = panel;
            panel.Init();
        }
        
        public void OnExitButtonClicked()
        {
            if (Application.isEditor)
                EditorApplication.isPlaying = false;
            else
                Application.Quit();
        }

        private void OnDestroy()
        {
            _inputActions.MenuButtonPressed -= OpenMenu;
            _inputActions.UIBackButtonPressed -= BackKeyPressed;
            _inputActions.UIApplyButtonPressed -= ApplyKeyPressed;
        }

        IUnitInput IUnitActionController.InputActions
        {
            get => _inputActions;
            set => _inputActions = value;
        }
    }
}