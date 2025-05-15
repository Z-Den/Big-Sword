using PivotConnection;
using Units.Enemy.EnemyItems;
using UnityEngine;

namespace Units.Enemy
{
    public class EnemyWithBothItem : Enemy
    {
        [SerializeField] private Item _handsPrefab;

        private void Start()
        {
            if (_handsPrefab == null)
                InitializeItem(_handsPrefab);
        }
    }
}