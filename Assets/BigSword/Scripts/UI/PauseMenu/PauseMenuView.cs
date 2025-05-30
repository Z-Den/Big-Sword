using System;
using System.Collections.Generic;
using SaveLoadSystem;
using Units.Input;
using Units.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PauseMenu
{
    public class PauseMenuView : UIElement, IUIElementHolder 
    {
        [Header("Panels")]
        [SerializeField] private List<MenuButton> _buttons;
        [SerializeField] private List<Image> _backgrounds;
        [SerializeField] private Image _panelsBackground;
        private MenuPanel _activePanel;
        
        public bool IsPanelOpened => _activePanel != null;
        
        public void SetActive(bool active)
        {
            foreach (var button in _buttons)
                button.gameObject.SetActive(active);
            foreach (var background in _backgrounds)
                background.gameObject.SetActive(active);
            ClosePanel();
        }

        public void ShowPanel(MenuPanel panel)
        {
            ClosePanel();
            panel.SetActive(true);
            _activePanel = panel;
            
            SetElementState(IsPanelOpened);
        }

        public void ClosePanel()
        {
            if (_activePanel != null)
            {
                _activePanel.SetActive(false);
                _activePanel = null;
            }

            SetElementState(IsPanelOpened);
        }
        
        private void SetElementState(bool isPanelOpened)
        {
            foreach (var button in _buttons)
                button.interactable = !isPanelOpened;
            _panelsBackground.enabled = isPanelOpened;
        }
        
        public UIElement GetUIElement()
        {
            return this;
        }
    }
}