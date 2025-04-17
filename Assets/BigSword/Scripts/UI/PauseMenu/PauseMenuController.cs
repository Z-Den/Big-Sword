using System;
using System.Collections.Generic;
using Units.Input;
using Units.UI;
using Unity.VisualScripting;
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

            SubscribeToButtons();
            
            _inputActions.MenuButtonPressed += OpenMenu;
            _inputActions.UIBackButtonPressed += BackButtonPressed;
            _inputActions.UIApplyButtonPressed += ApplyButtonPressed;
            
            CloseMenu();
        }

        private void SubscribeToButtons()
        {
            foreach (var button in _view.Buttons)
                button.onClick.AddListener(OnPointerClickButton);
        }

        private void OnPointerClickButton()
        {
            
        }

        private void ApplyButtonPressed()
        {
           
        }

        private void BackButtonPressed()
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

        public void PressButton()
        {
            
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

        private void OnDisable()
        {
            UnsubscribeFromButtons();
        }

        private void UnsubscribeFromButtons()
        {
            foreach (var button in _view.Buttons)
                button.onClick.RemoveListener(OnPointerClickButton);
        }
        
        IUnitInput IUnitActionController.InputActions
        {
            get => _inputActions;
            set => _inputActions = value;
        }
    }
}