using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.PauseMenu
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private List<ButtonRenderer> _buttons;

        private void Awake()
        {
            ResetButtons();
        }

        public void PressButton(ButtonRenderer clickedButton)
        {
            ResetButtons();
            Debug.Log(clickedButton);
            clickedButton.SetBackgroundColor(new Color(1, 1, 1, 1));
        }

        private void ResetButtons()
        {
            foreach (var button in _buttons)
                button.SetBackgroundColor(new Color(0, 0, 0, 0));
        }
    }
}