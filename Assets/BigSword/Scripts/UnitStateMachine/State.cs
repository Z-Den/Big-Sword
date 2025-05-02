using System;
using System.Collections.Generic;
using UnitStateMachine.UpdateActions;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnitStateMachine
{
    [Serializable]
    public class State 
    {
        [SerializeField] private int _index;
        [SerializeField] private StateAction _enterActionPrefab;
        [SerializeField] private StateAction _updateActionPrefab;
        [SerializeField] private StateAction _exitActionPrefab;
        [SerializeField] private EnterTrigger _enterTriggerPrefab;
        private StateMachine _stateMachine;
        private StateAction _updateAction;
        private StateAction _enterAction;
        private StateAction _exitAction;
        private EnterTrigger _enterTrigger;
        
        public int Index => _index;

        public void Init(StateMachine stateMachine)
        {   
            _stateMachine = stateMachine;
            
            _updateAction = GetInstance(_updateActionPrefab);
            _enterAction = GetInstance(_enterActionPrefab);
            _exitAction = GetInstance(_exitActionPrefab);
            
            _enterTrigger = GetInstance(_enterTriggerPrefab);  
        }
        
        public void OnEnter()
        {
            _enterAction.DoAction();
        }

        public void OnUpdate()
        {
            _updateAction.DoAction();
        }

        public bool IsTransitionReady()
        {
            return _enterTrigger.IsReadyToTransit();
        }
        
        public void OnExit()
        {
            _exitAction.DoAction();
        }
        
        private StateAction GetInstance(StateAction prefab)
        {
            var instance = ScriptableObject.CreateInstance(prefab.GetType()) as StateAction;
            if (instance != null)
                instance?.Init(_stateMachine);
            else
                instance = ScriptableObject.CreateInstance<DoNothing>();
            return instance;
        }
        
        private EnterTrigger GetInstance(EnterTrigger prefab)
        {
            var instance = ScriptableObject.CreateInstance(prefab.GetType()) as EnterTrigger;
            if (instance != null)
                instance?.Init(_stateMachine);
            else
                throw new Exception("Trigger is empty");
            return instance;
        }
    }

    [Serializable]
    public struct Transition
    {
        public Vector2Int FromTo; 
        public int FromStateIndex => FromTo.x;
        public int NextStateIndex => FromTo.y;
    }
}