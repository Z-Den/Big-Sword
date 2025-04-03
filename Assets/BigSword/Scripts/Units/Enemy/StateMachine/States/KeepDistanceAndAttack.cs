using UnityEngine;

namespace Units.Enemy.StateMachine.States
{
    public class KeepDistanceAndAttack : Follow
    {
        private float _maxDistance;
        private float _minDistance;
        
        public KeepDistanceAndAttack(Enemy enemy) : base(enemy)
        {
            _maxDistance = Enemy.Parameters.CheckSphereRadius * 2;
            _minDistance = Enemy.Parameters.MinDistance;
        }

        public override void OnUpdate()
        {
            var directionToTarget = Taget.position - Enemy.transform.position;
            var distance = directionToTarget.magnitude;
            var moveDirection = directionToTarget.normalized * (distance - _minDistance);
            
            if (distance > _maxDistance)
                IsStateChange?.Invoke();
            
            Enemy.Mover.SetMoveDirection(moveDirection);
            Enemy.Mover.SetRotation(Quaternion.LookRotation(directionToTarget));
        }
    }
}