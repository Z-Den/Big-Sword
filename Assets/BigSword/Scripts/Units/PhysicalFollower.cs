using System;
using PivotConnection;
using UnityEngine;

namespace Units
{
    public class PhysicalFollower : MonoBehaviour, IPivotFollower
    {
        [Header("Pivot")]
        [SerializeField] private Vector3 _offset;
        [Header("Speed")]
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private float _accelerationPower = 20f;
        [SerializeField] private float _maxAcceleration = 100f;
        [SerializeField] private AnimationCurve _accelerationFromDot;
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
        private Transform _pivotTransform;

        Vector3 IPivotFollower.Offset => _offset;
        Transform IPivotFollower.PivotTransform
        {
            get => _pivotTransform;
            set => _pivotTransform = value;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _direction = (_pivotTransform.position + _offset) - transform.position;
            _targetRotation = _pivotTransform.rotation;
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