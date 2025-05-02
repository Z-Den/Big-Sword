using System;
using System.Collections.Generic;
using PivotConnection;
using Units.Health;
using UnityEngine;

namespace Units.Player
{
    public class Player : Unit, IPivotHolder 
    {
        [SerializeField] private Transform _pivotTransform;
        [SerializeField] private List<Pivot> _pivotList;
        
        public List<Pivot> PivotList => _pivotList;
        public UnitHealth Health => GetComponent<UnitHealth>();

        private void Start()
        {
            Health.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            GameManager.GameManager.Instance.ShowDeadPanel();
        }

    }
}
