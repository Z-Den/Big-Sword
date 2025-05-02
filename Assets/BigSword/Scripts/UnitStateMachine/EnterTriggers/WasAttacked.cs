using UnityEngine;

namespace UnitStateMachine.EnterTriggers
{
    [CreateAssetMenu(menuName = "State Machine/Enter Trigger/Was Attacked")]
    public class WasAttacked : EnterTrigger
    {
        private bool _isAttacking = false;
        
        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            Enemy.Health.DamageApplied += DamageApplied;
        }

        private void DamageApplied(float obj)
        {
            _isAttacking = true;
        }

        public override bool IsReadyToTransit()
        {
            return _isAttacking;
        }
    }
}