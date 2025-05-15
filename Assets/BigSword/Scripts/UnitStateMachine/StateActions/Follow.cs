using UnityEngine;

namespace UnitStateMachine.UpdateActions
{
    [CreateAssetMenu(menuName = "State Machine/Update Actions/Follow")]
    public class Follow : StateAction
    {
        private float _checkSphereRange;
        private LayerMask _respondMask;
        private Transform _target;

        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            _checkSphereRange = Enemy.Parameters.CheckSphereRadius;
            _respondMask = Enemy.Parameters.RespondMask;
        }

        public override void DoAction()
        {
            if (_target == null)
                _target = GetTarget();
            
            var direction = _target.position - Enemy.transform.position;
            var moveInput = new Vector2(direction.x, direction.z);
            StateMachine.MoveDirection = Vector2.up;
            var curDir = new Vector2(Enemy.transform.forward.x, Enemy.transform.forward.z);
            StateMachine.Rotation = Vector2.SignedAngle(moveInput.normalized, curDir.normalized) * Time.deltaTime * 10f;
        }
        
        private Transform GetTarget()
        {
            var colliders = Physics.OverlapSphere(Enemy.transform.position, _checkSphereRange * 1.1f, _respondMask);
            if (colliders.Length == 0)
                return null;

            var minDistance = float.MaxValue;
            Transform target = null;
            foreach (var collider in colliders)
            {
                var distance = Vector3.Distance(collider.transform.position, Enemy.transform.position);
                if (distance < minDistance)
                    target = collider.transform;
            }
            return target;
        }
    }
}