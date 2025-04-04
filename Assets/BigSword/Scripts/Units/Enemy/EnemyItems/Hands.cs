using System;
using Units.Enemy.StateMachine;
using Units.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Enemy.EnemyItems
{
    public class Hands : MonoBehaviour
    {
        private static readonly int IsDanger = Animator.StringToHash("IsDanger");
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        [SerializeField] private float _height;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 20f;
        [SerializeField] private Transform _animatedLeftHand;
        [SerializeField] private Transform _animatedRightHand;
        [SerializeField] private Transform _physicalLeftHand;
        [SerializeField] private Transform _physicalRightHand;
        private Animator _animator;
        private Transform _pivot;
        
        public void Init(Enemy enemy)
        {
            _animator = GetComponent<Animator>();
            _pivot = enemy.transform;
            var stateMachine = enemy.StateMachine;
            stateMachine.OnDefaultState += OnDefaultState;
            stateMachine.OnDangerState += OnDangerState;
            stateMachine.Enemy.Health.OnDeath += OnDeath;
            OnDefaultState();
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                _animator.SetTrigger(Attack);
        }

        private void OnDangerState()
        {
            _animator.SetBool(IsDanger, true);
        }

        private void OnDefaultState()
        {
            _animator.SetBool(IsDanger, false);
        }

        private void Update()
        {
            var position = _pivot.position;
            position.y = _height;
            transform.position = position;
            transform.rotation = _pivot.rotation;
            
            // LerpMove(_leftHand, _leftHandCurrentPosition);
            // LerpMove(_rightHand, _rightHandCurrentPosition);
            // LerpRotate(_leftHand, _leftHandCurrentPosition);            
            // LerpRotate(_rightHand, _rightHandCurrentPosition);
        }

        private void LerpMove(Transform hand, Transform targetPosition)
        {
            hand.position = Vector3.Lerp(hand.position, targetPosition.position, Time.deltaTime * _moveSpeed);
        }

        private void LerpRotate(Transform hand, Transform targetPosition)
        {
            hand.rotation = Quaternion.Lerp(hand.rotation, targetPosition.rotation, Time.deltaTime * _rotationSpeed);
        }
    }
}