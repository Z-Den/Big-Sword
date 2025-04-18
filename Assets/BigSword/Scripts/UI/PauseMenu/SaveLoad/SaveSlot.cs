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

        public Action OnSaveButtonPressed;
        
        public void Render(Sprite sprite, string name, DateTime date)
        {
            _image.sprite = sprite;
            _name.text = name;
            _date.text = date.ToShortDateString();
        }
        
        public void Render(Sprite sprite, string name)
        {
            _image.sprite = sprite;
            _name.text = name;
            _date.text = "";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSaveButtonPressed?.Invoke();
        }
    }
}