using System;
using UnityEngine;

namespace Units.Enemy
{
    [Serializable]
    public struct EnemyParameters
    {
        public float CheckSphereRadius;
        public float CheckDelay;
        public float MinDistance;
        public LayerMask RespondMask;
        public Vector3[] Path;
    }
}