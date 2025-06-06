using System;
using PivotConnection;
using UnityEngine;

namespace Units.Player
{
    public class CameraFollower : MonoBehaviour, IPivotFollower
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _rotationOffset;
        private Transform _pivotTransform;
        private string _pivotKey;

        string IPivotFollower.PivotKey => "camera";

        Transform IPivotFollower.PivotTransform
        {
            get => _pivotTransform;
            set => _pivotTransform = value;
        }

        public Vector3 Offset => _offset;

        private void Update()
        {
            if (!_pivotTransform)
                return;
            
            var position = _pivotTransform.position + _pivotTransform.forward * Offset.z; 
            position.y = Offset.y;
            transform.position = position;
            transform.rotation = Quaternion.Euler(_rotationOffset.x, _pivotTransform.localEulerAngles.y, _rotationOffset.z); 
        }

    }
}