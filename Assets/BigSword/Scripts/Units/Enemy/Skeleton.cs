using PivotConnection;
using Units.Enemy.EnemyItems;
using UnityEngine;

namespace Units.Enemy
{
    public class Skeleton : Enemy
    {
        [SerializeField] private Hands _handsPrefab;

        private void Start()
        {
            var hands = Instantiate(_handsPrefab);
            hands.transform.position = transform.position;
            ((IPivotFollower)hands.HandsFollower).SetPivot(this);
            hands.Init(this);
        }
    }

}
    