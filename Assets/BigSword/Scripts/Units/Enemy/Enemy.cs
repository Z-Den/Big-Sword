using System.Collections.Generic;
using PivotConnection;
using Units.Enemy.StateMachine;
using Units.Health;
using Units.Input;
using UnityEngine;
using IUIElementHolder = Units.UI.IUIElementHolder;

namespace Units.Enemy
{
    public class Enemy : Unit, IPivotHolder
    {
        [SerializeField] private PhysicalMover _mover;
        [SerializeField] private EnemyParameters _parameters;
        [SerializeField] private UnitStateMachine.StateMachine _stateMachine;
        [SerializeField] private UnitUI _unitUI;
        [SerializeField] private UnitHealth _health;
        [SerializeField] private List<Pivot> _pivotList;

        public List<Pivot> PivotList => _pivotList;
        public PhysicalMover Mover => _mover;
        public EnemyParameters Parameters => _parameters;
        public UnitStateMachine.StateMachine StateMachine => _stateMachine;
        public UnitUI UnitUI => _unitUI;
        public UnitHealth Health => _health;
        
        protected void Awake()
        { 
            _stateMachine.Init(this);
            (Mover as IUnitActionController).SetInput(StateMachine); 
            AddUI();
        }

        private void AddUI()
        {
            foreach (var uiHolder in GetComponents<IUIElementHolder>())
            {
                var element = uiHolder.GetUIElement();
                if (element != null)
                    UnitUI.Add(element);
            }
        }

        protected virtual void Update()
        {
            _stateMachine.Update();
        }

    }
}
