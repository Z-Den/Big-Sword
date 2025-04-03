using System;
using Units.Enemy.StateMachine;
using Units.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Enemy.EnemyItems
{
    public class Hands : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 20f;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;
        [Header("Hands Transform")]
        [SerializeField] private Transform _leftHandIdlePosition;
        [SerializeField] private Transform _rightHandIdlePosition;
        [SerializeField] private Transform _leftHandDangerPosition;
        [SerializeField] private Transform _rightHandDangerPosition;
        private Transform _leftHandCurrentPosition;
        private Transform _rightHandCurrentPosition;
        private Transform _pivot;
        
        public void Init(Enemy enemy)
        {
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

        private void OnDangerState()
        {
            _leftHandCurrentPosition = _leftHandDangerPosition;
            _rightHandCurrentPosition = _rightHandDangerPosition;
        }

        private void OnDefaultState()
        {
            _leftHandCurrentPosition = _leftHandIdlePosition;
            _rightHandCurrentPosition = _rightHandIdlePosition;
        }

        private void Update()
        {
            transform.position = _pivot.position;
            transform.rotation = _pivot.rotation;
            
            LerpMove(_leftHand, _leftHandCurrentPosition);
            LerpMove(_rightHand, _rightHandCurrentPosition);
            LerpRotate(_leftHand, _leftHandCurrentPosition);            
            LerpRotate(_rightHand, _rightHandCurrentPosition);
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