using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Units.Input
{
    public class PlayerInput : IUnitInput
    {
        private PlayerInputAction _playerInGameInput;
        private PlayerInputAction _playerUIInput;
        
        public Vector2 MoveDirection {get; set;}
        public float Rotation {get; set;}
        public Action ShotStarted {get; set;}
        public Action ShotCanceled {get; set;}
        public Action Spell1Started {get; set;}
        public Action Spell1Canceled {get; set;}
        public Action RunStarted {get; set;}
        public Action RunCanceled {get; set;}
        public Action DashStarted { get; set; }
        public Action MenuButtonPressed { get; set; }
        public Action UIBackButtonPressed { get; set; }
        public Action UIApplyButtonPressed { get; set; }

        public PlayerInput()
        {
            InitInGameInput();
            InitUIInput();
        }

        private void InitInGameInput()
        {
            _playerInGameInput = new PlayerInputAction();
            
            _playerInGameInput.Player.Move.started += _ => MoveDirection = _playerInGameInput.Player.Move.ReadValue<Vector2>();
            _playerInGameInput.Player.Rotation.started += _ => Rotation = _playerInGameInput.Player.Rotation.ReadValue<float>();
            
            _playerInGameInput.Player.Shot.started += _ => ShotStarted?.Invoke();
            _playerInGameInput.Player.Shot.canceled += _ => ShotCanceled?.Invoke();
            
            _playerInGameInput.Player.Shield.started += _ => Spell1Started?.Invoke();
            _playerInGameInput.Player.Shield.canceled += _ => Spell1Canceled?.Invoke();
            
            _playerInGameInput.Player.Run.started += _ => RunStarted?.Invoke();
            _playerInGameInput.Player.Run.canceled += _ => RunCanceled?.Invoke();
            
            _playerInGameInput.Player.Dash.started += _ => DashStarted?.Invoke();
        }

        private void InitUIInput()
        {
            _playerUIInput = new PlayerInputAction();
            _playerUIInput.UI.MenuButton.started += _ => MenuButtonPressed?.Invoke();
            _playerUIInput.UI.Back.started += _ => UIBackButtonPressed?.Invoke();
            _playerUIInput.UI.MenuButton.started += _ => UIApplyButtonPressed?.Invoke();
        }
        
        public void SetInputScheme(PlayerInputScheme scheme)
        {
            switch (scheme)
            {
                case PlayerInputScheme.Battle:
                    _playerInGameInput.Enable();
                    _playerUIInput.Disable();
                    break;
                case PlayerInputScheme.UI:
                    _playerInGameInput.Disable();
                    _playerUIInput.Enable();
                    break;
                case PlayerInputScheme.None:
                    _playerInGameInput.Disable();
                    _playerUIInput.Disable();
                    break;
            }
        }
    }
    
    
}
