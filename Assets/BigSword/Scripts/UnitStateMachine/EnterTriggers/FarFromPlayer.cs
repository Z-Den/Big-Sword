using UnityEngine;

namespace UnitStateMachine.EnterTriggers
{
    [CreateAssetMenu(menuName = "State Machine/Enter Trigger/Far From Player")]
    public class FarFromPlayer : EnterTrigger
    {
        private float _checkSphereRange;
        private LayerMask _respondMask;
        
        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            _checkSphereRange = Enemy.Parameters.CheckSphereRadius;
            _respondMask = Enemy.Parameters.RespondMask;
        }

        public override bool IsReadyToTransit()
        {
            return !CheckPlayer();
        }
        
        private bool CheckPlayer()
        {
            var colliders = Physics.OverlapSphere(Enemy.transform.position, _checkSphereRange * 2, _respondMask);
            return colliders.Length > 0;
        }
    }
}