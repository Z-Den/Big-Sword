using UnityEngine;

namespace UnitStateMachine.EnterTriggers
{
    [CreateAssetMenu(menuName = "State Machine/Enter Trigger/Close to Player")]

    public class CloseToPlayer : EnterTrigger
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
            return CheckPlayer();
        }
        
        private bool CheckPlayer()
        {
            var colliders = Physics.OverlapSphere(Enemy.transform.position, _checkSphereRange, _respondMask);
            return colliders.Length > 0;
        }
    }
}
