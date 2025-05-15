using System;
using Units;
using Units.Enemy;
using UnityEngine;

namespace UnitStateMachine
{
    public abstract class EnterTrigger : ScriptableObject
    {
        protected StateMachine StateMachine;
        protected Enemy Enemy;
        
        public virtual void Init(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
            Enemy = StateMachine.Enemy;
        }
        
        public abstract bool IsReadyToTransit(); 
    }
}