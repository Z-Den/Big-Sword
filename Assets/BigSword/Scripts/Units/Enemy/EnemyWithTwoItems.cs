using PivotConnection;
using Units.Enemy.EnemyItems;
using UnityEngine;

namespace Units.Enemy
{
    public class EnemyWithTwoItems : Enemy
    {
        [SerializeField] private Item _leftHandItemPrefab;
        [SerializeField] private Item _rightHandItemPrefab;

        private void Start()
        {
            if (_leftHandItemPrefab != null) InitializeItem(_leftHandItemPrefab);
            if (_rightHandItemPrefab != null) InitializeItem(_rightHandItemPrefab);       
        }
    }
}