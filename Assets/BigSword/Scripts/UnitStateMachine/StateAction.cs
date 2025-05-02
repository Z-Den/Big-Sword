using Units.Enemy;
using UnityEngine;

namespace UnitStateMachine
{
    public abstract class StateAction : ScriptableObject
    {
        protected StateMachine StateMachine;
        protected Enemy Enemy;

        public virtual void Init(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
            Enemy = StateMachine.Enemy;
        }
        
        public abstract void DoAction();
    }
}