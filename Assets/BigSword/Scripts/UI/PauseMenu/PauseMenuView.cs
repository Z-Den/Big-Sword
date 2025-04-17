using System;
using System.Collections.Generic;
using Units.Input;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PauseMenu
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttons;
        [SerializeField] private Image _background;
        private GameObject _activePanel;
        
        public bool IsPanelOpened => _activePanel != null;
        public List<Button> Buttons => _buttons;

        public void SetActive(bool active)
        {
            foreach (var button in _buttons)
                button.gameObject.SetActive(active);
            _background.gameObject.SetActive(active);
            ClosePanel();
        }

        public void ShowPanel(GameObject panel)
        {
            ClosePanel();
            panel.SetActive(true);
            _activePanel = panel;
        }

        public void ClosePanel()
        {
            if (_activePanel != null)
            {
                _activePanel.SetActive(false);
                _activePanel = null;
            }
        }
    }
}