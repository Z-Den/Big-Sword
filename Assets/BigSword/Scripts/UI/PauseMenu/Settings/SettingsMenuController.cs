using System;
using UnityEngine;

namespace UI.PauseMenu.Settings
{
    public class SettingsMenuController : MenuPanel
    {
        private SettingsMenuView _view;
        private SettingsMenuModel _model;

        public void OpenPanel(GameObject panel)
        {
            _view.OpenPanel(panel);
        }

        public override void Init()
        {
            _model ??= new SettingsMenuModel();
            _view ??= GetComponent<SettingsMenuView>();
        }

        public override void BackButtonPressed(Action noInteractionCallback)
        {
            if (_view.IsPanelOpen)
                _view.ClosePanel();
            else
                noInteractionCallback?.Invoke();
        }
    }
}