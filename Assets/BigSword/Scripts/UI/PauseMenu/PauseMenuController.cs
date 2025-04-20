using System;
using SaveLoadSystem;
using Units.Input;
using Units.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PauseMenu
{
    public class PauseMenuController : UIElement, IUIElementHolder, IUnitActionController
    {
        private PauseMenuModel _model;
        private PauseMenuView _view;
        private IUnitInput _inputActions;

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
            if (_view.IsPanelOpened)
                _view.ClosePanel();
            else 
                CloseMenu();
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
        
        public UIElement GetUIElement()
        {
            return this;
        }
        
        public void OnContinueButtonClicked()
        {
            CloseMenu();
        }

        public void OnPanelButtonClicked(GameObject panel)
        {
            _view.ShowPanel(panel);
        }
        
        public void OnExitButtonClicked()
        {
            if (Application.isEditor)
                UnityEditor.EditorApplication.isPlaying = false;
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