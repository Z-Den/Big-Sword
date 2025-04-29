using System;
using UnityEngine;

namespace UI.PauseMenu.SaveLoad
{
    public class SaveLoadController : MenuPanel
    {
        private SaveLoadModel _saveLoadModel;
        private SaveLoadView _saveLoadView;

        private void SubscribeToSlots()
        {
            if (_saveLoadView.EmptySlot)
                _saveLoadView.EmptySlot.OnSaveButtonPressed += OnNewSaveButtonPressed;
            
            foreach (var slot in _saveLoadView.Slots)
            {
                slot.OnLoadButtonPressed += OnLoadButtonPressed;
                slot.OnSaveButtonPressed += OnSaveButtonPressed;
            }
        }

        public void OnLoadButtonPressed(string slotName)
        {
            _saveLoadModel.Load(slotName);
        }

        public void OnNewSaveButtonPressed()
        {
            OnConfirmButtonPressed();
        }

        public void OnSaveButtonPressed()
        {
            _saveLoadView.SetConfirmPanelVisible(true);
        }

        public void OnConfirmButtonPressed()
        {
            _saveLoadModel.Save();
            _saveLoadView.Render();
            SubscribeToSlots();
        }

        public override void Init()
        {
            _saveLoadModel ??= new SaveLoadModel();
            _saveLoadView ??= GetComponent<SaveLoadView>();
            SubscribeToSlots();
        }
        

        public override void BackButtonPressed(Action noInteractionCallback)
        {
            if (_saveLoadView.IsConfirmPanelOpened)
                _saveLoadView.SetConfirmPanelVisible(false);
            else
                noInteractionCallback?.Invoke();
        }
    }
}