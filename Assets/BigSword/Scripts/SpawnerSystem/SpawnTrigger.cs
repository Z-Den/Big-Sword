using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnerSystem
{
    public class SpawnTrigger : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        public List<Transform> SpawnPoints => _spawnPoints;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                MobSpawner.Instance.PlayerOnTrigger(this);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                MobSpawner.Instance.PlayerFromTrigger(this);
        }
    }
}