using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.PauseMenu.SaveLoad
{
    public class SaveSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _date;
        private bool _isSaveButton;
        
        public Action OnSaveButtonPressed;
        public Action<string> OnLoadButtonPressed;
        
        public void Render(Sprite sprite, string fileName, DateTime date, bool isSaveButton)
        {
            _image.sprite = sprite;
            _name.text = fileName;
            _date.text = date.ToString();
            _isSaveButton = isSaveButton;
        }
        
        public void Render(Sprite sprite, string fileName)
        {
            _image.sprite = sprite;
            _name.text = fileName;
            _date.text = "";
            _isSaveButton = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isSaveButton)
                OnSaveButtonPressed?.Invoke();
            else
                OnLoadButtonPressed?.Invoke(_name.text);
        }
    }
}