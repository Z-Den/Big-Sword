using System;
using Units.Enemy.StateMachine.States;
using Units.Input;
using UnityEngine;

namespace Units.Enemy.StateMachine
{
    [Serializable]
    public class EnemyStateMachine : IUnitInput
    {
        public Vector2 MoveDirection { get; set; }
        public float Rotation { get; set; }
        public Action ShotStarted { get; set; }
        public Action ShotCanceled { get; set; }
        public Action Spell1Started { get; set; }
        public Action Spell1Canceled { get; set; }
        public Action RunStarted { get; set; }
        public Action RunCanceled { get; set; }
        public Action DashStarted { get; set; }
        public Action MenuButtonPressed { get; set; }
        public Action UIBackButtonPressed { get; set; }
        public Action UIApplyButtonPressed { get; set; }
        
        [SerializeField] private DefaultStateType _defaultStateType;
        [SerializeField] private DangerStateType _dangerStateTypeState;
        private State _defaultState;
        private State _dangerState;
        private State _currentState;

        public Enemy Enemy {get; private set;}
        public Action OnDangerState;
        public Action OnDefaultState;
        
        public void Init(Enemy enemy)
        {
            Enemy = enemy;
            switch (_defaultStateType)
            {
                case DefaultStateType.Off:
                    _defaultState = new OffState(this);
                    break;
                case DefaultStateType.Idle:
                    _defaultState = new Idle(this);
                    break;
                case DefaultStateType.Patrol:
                    _defaultState = new Patrol(this);
                    break;
                case DefaultStateType.RoombaPatrol:
                    _defaultState = new RoombaPatrol(this);
                    break;
            }   
            
            switch (_dangerStateTypeState)
            {
                case DangerStateType.Follow:
                    _dangerState = new Follow(this);
                    break;
                case DangerStateType.KeepDistanceAndAttack:
                    _dangerState = new KeepDistanceAndAttack(this);
                    break;
                case DangerStateType.RunAway:
                    break;
            }   
            ChangeState();
        }

        public void Update()
        {
            _currentState?.OnUpdate();
        }

        public void ChangeState()
        {
            _currentState?.OnExit();
            _currentState = _currentState == _defaultState ? _dangerState : _defaultState;
            _currentState?.OnEnter();
            
            if (_currentState == _defaultState)
                OnDefaultState?.Invoke();
            else
                OnDangerState?.Invoke();
        }
    }
}