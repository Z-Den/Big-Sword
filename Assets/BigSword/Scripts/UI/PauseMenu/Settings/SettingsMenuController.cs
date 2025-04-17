using System;
using UnityEngine;

namespace UI.PauseMenu.Settings
{
    public class SettingsMenuController : MonoBehaviour
    {
        private SettingsMenuView _view;
        private SettingsMenuModel _model;
        
        private void Start()
        {
            _view = GetComponent<SettingsMenuView>();
            _model = new SettingsMenuModel();
        }

        public void OpenPanel(GameObject panel)
        {
            _view.OpenPanel(panel);
        }
    }
}