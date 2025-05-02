using System.Collections.Generic;
using PivotConnection;
using UnityEngine;

namespace Units.Enemy.EnemyItems
{
    public class Hands : MonoBehaviour, IPivotHolder
    {
        private static readonly int IsDanger = Animator.StringToHash("IsDanger");
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        [SerializeField] private PhysicalFollower _physicalLeftHand;
        [SerializeField] private PhysicalFollower _physicalRightHand;
        [SerializeField] private List<Pivot> _pivotList;
        private Animator _animator;
        
        public List<Pivot> PivotList => _pivotList;
        public PhysicalFollower HandsFollower => GetComponent<PhysicalFollower>();
        
        public void Init(Enemy enemy)
        {
            _animator = GetComponent<Animator>();
            var stateMachine = enemy.StateMachine;
            stateMachine.DangerStateChanged += DungerStateChanged;
            stateMachine.Enemy.Health.OnDeath += OnDeath;
            ((IPivotFollower)_physicalLeftHand).SetPivot(this);
            ((IPivotFollower)_physicalRightHand).SetPivot(this);
            DungerStateChanged(false);
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player _))
                _animator.SetTrigger(Attack);
        }
        
        private void DungerStateChanged(bool isDanger)
        {
            _animator.SetBool(IsDanger, isDanger);
        }
    }
}