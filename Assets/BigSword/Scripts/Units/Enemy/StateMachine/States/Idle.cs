using UnityEngine;

namespace Units.Enemy.StateMachine.States
{
    public class Idle : OffState
    {
        private float _timer = 0;
        private readonly float _checkDelay;
        private readonly float _checkSphereRange;
        private readonly LayerMask _respondMask;
        
        public Idle(EnemyStateMachine stateMachine) : base(stateMachine)
        {
            _checkSphereRange = stateMachine.Enemy.Parameters.CheckSphereRadius;
            _checkDelay = stateMachine.Enemy.Parameters.CheckDelay;
            _respondMask = stateMachine.Enemy.Parameters.RespondMask;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            SetTimer();
        }

        protected void SetTimer()
        {
            if (_timer < 0)
            {
                _timer = _checkDelay;
                CheckPlayer();
            }

            _timer -= Time.deltaTime;
        }

        private void CheckPlayer()
        {
            var colliders = Physics.OverlapSphere(Enemy.transform.position, _checkSphereRange, _respondMask);
            if (colliders.Length > 0)
            {
                StateMachine.ChangeState();
            }
        }
    }
}