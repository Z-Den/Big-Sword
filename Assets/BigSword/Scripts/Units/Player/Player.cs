using PivotConnection;
using Units.Input;
using UnityEngine;

namespace Units.Player
{
    public class Player : Unit, IPivot 
    {
        [SerializeField] private Transform _pivotTransform;

        public Transform PivotTransform => _pivotTransform;
        
    }
}
