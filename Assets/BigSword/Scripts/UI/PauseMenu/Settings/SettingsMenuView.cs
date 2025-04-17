using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.PauseMenu.Settings
{
    public class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private List<MenuButton> _buttons;
        private MenuButton _currentButton;
        private GameObject _currentPanel;

        private void OnEnable()
        {
            ClosePanel();
        }
        
        public void OpenPanel(GameObject panel)
        {
            if (_currentPanel != null)
                ClosePanel();
            
            _currentPanel = panel;
            _currentPanel.SetActive(true);
        }

        private void ClosePanel()
        {
            if (_currentPanel == null) return;
            
            _currentPanel.SetActive(false);
            _currentPanel = null;
        }
    }
}