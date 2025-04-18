using System;
using System.Collections.Generic;
using System.Linq;
using SaveLoadSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.PauseMenu.SaveLoad
{
    public class SaveLoadView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private bool _isInstantiateEmptySlot; 
        [SerializeField] private SaveSlot _cellPrefab;
        
        public SaveSlot EmptySlot { get; private set; }
        public List<SaveSlot> Slots { get; private set; }
        
        private void OnEnable()
        {
            Render();
        }

        public void Render()
        {
            ClearContainer();
            if (_isInstantiateEmptySlot)
            {
                var instance = Instantiate(_cellPrefab, _container);
                instance.Render(null, "New save");
                EmptySlot = instance;
            }
            Fill();
        }

        private void Fill()
        {
            var saveFiles = SaveLoadService.Instance.GetSaveFilesList();
            saveFiles.Reverse();
            foreach (var saveFile in saveFiles)
            {
                var instance = Instantiate(_cellPrefab, _container);
                instance.Render(null, saveFile.SaveName, saveFile.SaveDate);
                Slots.Add(instance);
            }
        }

        private void ClearContainer()
        {
            Slots = new List<SaveSlot>();
            foreach(Transform child in _container.transform)
                Destroy(child.gameObject);
        }

        public void ShowConfirmPanel()
        {
            
        }
    }
}