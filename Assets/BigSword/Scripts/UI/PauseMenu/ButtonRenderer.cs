using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.PauseMenu
{
    public class ButtonRenderer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")]
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _background;
        [Header("Color")]
        [SerializeField] private Color _defaultTextColor;
        [SerializeField] private Color _selectedTextColor;        
        [SerializeField] private Color _defaultBackgroundColor;
        [SerializeField] private Color _selectedBackgroundColor;
        
        public void SetTextColor(Color color)
        {
            _text.color = color;
        }

        public void SetBackgroundColor(Color color)
        {
            _background.color = color;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            SetTextColor(_selectedTextColor);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetTextColor(_defaultTextColor);
        }
    }
}