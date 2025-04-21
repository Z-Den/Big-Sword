using System;
using Units.Enemy.EnemyItems;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Enemy
{
    public class Ogr : Enemy
    {
        [SerializeField] private Hands _handsPrefab;

        private void Start()
        {
            var hands = Instantiate(_handsPrefab);
            hands.Init(this);
        }
    }
}