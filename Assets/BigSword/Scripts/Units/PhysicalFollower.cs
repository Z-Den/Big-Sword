using System;
using UnityEngine;

namespace Units
{
    public class PhysicalFollower : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private float _accelerationPower = 20f;
        [SerializeField] private float _maxAcceleration = 100f;
        [SerializeField] private AnimationCurve _accelerationFromDot;
        [SerializeField] private Transform _pivot;
        [Header("Spring")] 
        [SerializeField] private float _rotationSpringStrength = 100f;
        [SerializeField] private float _rotationSpringDamper = 20f;
        private Quaternion _targetRotation;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private Vector3 _moveVector;
        private Vector3 _goalVelocity;
        private float _degree;
        private float _stamina;
        private bool _isRunning;
        private bool _isDashing;
        private Vector3 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _direction = _pivot.position - transform.position;
            _targetRotation = _pivot.rotation;
        }

        private void FixedUpdate()
        {
            HorizontalMove();
            RotationStabilization();
        }

        private void HorizontalMove()
        {
            var move = _direction;
            if (move.magnitude > 1f) move.Normalize();
            
            var velocityDot = Vector3.Dot(move, _rigidbody.linearVelocity);
            var acceleration = _accelerationPower * _accelerationFromDot.Evaluate(velocityDot);

            var velocity = move * _maxSpeed;
            _goalVelocity = Vector3.MoveTowards(_goalVelocity, velocity, acceleration * Time.fixedDeltaTime);

            var neededAccel = (_goalVelocity - _rigidbody.linearVelocity) / Time.fixedDeltaTime;
            neededAccel = Vector3.ClampMagnitude(neededAccel, _maxAcceleration);
            _rigidbody.AddForce(neededAccel * _rigidbody.mass);
        }
        

        private void RotationStabilization()
        {
            var toGoal = _targetRotation * Quaternion.Inverse(_rigidbody.transform.rotation);

            toGoal.ToAngleAxis(out var rotDegrees, out var rotAxis);
            rotAxis.Normalize();

            if (rotDegrees > 180f)
                rotDegrees -= 360f;
            
            var rotRadians = rotDegrees * Mathf.Deg2Rad;
            
            _rigidbody.AddTorque((rotAxis * (rotRadians * _rotationSpringStrength)) -
                                 (_rigidbody.angularVelocity * _rotationSpringDamper));
        }
    }
}