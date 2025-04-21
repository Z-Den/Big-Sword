using System;
using UnityEngine;

namespace UI.PauseMenu
{
    public abstract class MenuPanel : MonoBehaviour
    {
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        
        public abstract void Init();

        public abstract void BackButtonPressed(Action noInteractionCallback);
    }
}