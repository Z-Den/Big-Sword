using System;
using UnityEngine;

namespace UI.PauseMenu.SaveLoad
{
    public class SaveLoadController : MonoBehaviour
    {
        private SaveLoadModel _saveLoadModel;
        private SaveLoadView _saveLoadView;
        
        private void Start()
        {
            _saveLoadModel = new SaveLoadModel();
            _saveLoadView = GetComponent<SaveLoadView>();
            SubscribeToSlots();
        }

        private void SubscribeToSlots()
        {
            if (_saveLoadView.EmptySlot)
                _saveLoadView.EmptySlot.OnSaveButtonPressed += OnSaveButtonPressed;
            
            foreach (var slot in _saveLoadView.Slots)
            {
                slot.OnSaveButtonPressed += OnSaveButtonPressed;
            }
        }

        public void OnSaveButtonPressed()
        {
            //_saveLoadView.ShowConfirmPanel();
            _saveLoadModel.Save();
            _saveLoadView.Render();
        }

        public void OnConfirmButtonPressed()
        {
            _saveLoadModel.Save();
        }
    }
}