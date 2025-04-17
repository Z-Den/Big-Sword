using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.PauseMenu
{
    public class MenuButton : Button
    {
        private TMP_Text _text;

        protected override void Awake()
        {
            base.Awake();
            _text = targetGraphic as TMP_Text;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (!interactable) return;
            if (_text == null) return;
            
            _text.fontStyle = FontStyles.Bold;
            _text.color = colors.highlightedColor;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            if (!interactable) return;
            if (_text == null) return;
            
            _text.fontStyle = FontStyles.Normal;
            _text.color = colors.normalColor;
        }

        // public override void OnPointerClick(PointerEventData eventData)
        // {
        //     base.OnPointerClick(eventData);
        //     if (!interactable) return;
        //     if (_text == null) return;
        //     
        //     _text.fontStyle = FontStyles.Bold;
        //     _text.color = colors.normalColor;
        // }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            if (_text == null)
            {
                base.DoStateTransition(state, instant);
                return;
            }
                
            if (state == SelectionState.Disabled)
            {
                if (_text.fontStyle == FontStyles.Bold)
                {
                    _text.color = colors.normalColor;
                    return;
                }
            }

            if (state == SelectionState.Normal)
            {
                if (_text.fontStyle == FontStyles.Bold)
                {
                    _text.color = colors.normalColor;
                    _text.fontStyle = FontStyles.Normal;
                    return;
                }
            }
            base.DoStateTransition(state, instant);
        }
    }
}