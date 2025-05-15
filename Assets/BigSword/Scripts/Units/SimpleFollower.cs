using System;
using PivotConnection;
using UnityEngine;

namespace Units
{
    public class SimpleFollower : MonoBehaviour, IPivotFollower
    {
        [SerializeField] private string _pivotKey;
        private Transform _pivotTransform;

        string IPivotFollower.PivotKey => _pivotKey;

        Transform IPivotFollower.PivotTransform
        {
            get => _pivotTransform;
            set => _pivotTransform = value;
        }

        private void Update()
        {
            if (!_pivotTransform)
                return;
            
            transform.position = _pivotTransform.position;
            transform.rotation = _pivotTransform.rotation;
        }

    }
}