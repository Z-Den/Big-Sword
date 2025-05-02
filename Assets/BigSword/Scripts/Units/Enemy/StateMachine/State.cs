using System;
using JetBrains.Annotations;
using Units.Input;
using UnityEngine;

namespace Units.Enemy.StateMachine
{
    public abstract class State
    {
        protected Enemy Enemy;
        protected EnemyStateMachine StateMachine;
            
        public State(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            Enemy = StateMachine.Enemy;
        }
        
        public abstract void OnEnter(); 
        
        public abstract void OnUpdate();
        
        public abstract void OnExit();
    }
}