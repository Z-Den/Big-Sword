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
                slot.OnLoadButtonPressed += OnLoadButtonPressed;
                slot.OnSaveButtonPressed += OnSaveButtonPressed;
            }
        }

        public void OnLoadButtonPressed(string slotName)
        {
            _saveLoadModel.Load(slotName);
        }

        public void OnSaveButtonPressed()
        {
            //_saveLoadView.ShowConfirmPanel();
            _saveLoadModel.Save();
            _saveLoadView.Render();
            SubscribeToSlots();
        }

        public void OnConfirmButtonPressed()
        {
            _saveLoadModel.Save();
        }
    }
}