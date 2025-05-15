using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using Units.Enemy;
using Units.Input;
using UnityEngine;

namespace UnitStateMachine
{
    [Serializable]
    public class StateMachine : IUnitInput
    {
        #region IUnitInput
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
        #endregion
        
        [SerializeField] private List<State> _states;
        [SerializeField] private List<Transition> _transitions;
        
        private State _currentState;
        private List<State> _potentialNextStates = new List<State>();

        public Enemy Enemy { get; private set; }
        public Action<bool> DangerStateChanged;
        
        public void Init(Enemy enemy)
        {
            Enemy = enemy;
            
            if (_states.Any(state1 => _states.Where(state2 => !state1.Equals(state2)).Any(state2 => state1.Index == state2.Index)))
            {
                throw new Exception("The indexes match");
            }

            foreach (var state in _states)
            {
                state.Init(this);
            }
            
            ChangeState(0);
        }
        
        public void Update()
        {
            _currentState?.OnUpdate();
            CheckTransitions();
        }

        public void ChangeState(int newStateIndex)
        {
            _currentState?.OnExit();
            _currentState = GetStateByIndex(newStateIndex);
            _potentialNextStates = GetPotentialStates();
            _currentState.OnEnter();
        }

        private List<State> GetPotentialStates()
        {
            var transitions = _transitions.Where(x => x.FromStateIndex == _currentState.Index).ToList();
            return transitions.Select(transition => GetStateByIndex(transition.NextStateIndex)).ToList();
        }

        private void CheckTransitions()
        {
            foreach (var state in _potentialNextStates.Where(state => state.IsTransitionReady()))
            {
                ChangeState(state.Index);
            }
        }

        private State GetStateByIndex(int newStateIndex)
        {   
            foreach (var state in _states)
                if (state.Index == newStateIndex)
                    return state;
            throw new Exception("State index out of range.");
        }
    } 
}
