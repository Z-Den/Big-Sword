using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnerSystem
{
    public enum SpawnerType
    {
        RandomPoints,
        FixedPoints,
        Area
    }

    [Serializable]
    public class MobSpawnData
    {
        public GameObject _mobPrefab;
        [Range(0f, 1f)] public float _spawnChance = 0.5f;
        public bool _isRare = false;
        public int _minLevel = 1;
        public int _maxLevel = 10;
    }

    public class MobSpawner : MonoBehaviour
    {
        [Header("Spawner Settings")] [SerializeField]
        private SpawnerType _spawnerType;

        [SerializeField] private float _spawnInterval = 5f;
        [SerializeField] private int _maxMobs = 10;
        [SerializeField] private bool _active = false;

        [Header("Mob Settings")] [SerializeField]
        private List<MobSpawnData> _mobsToSpawn;

        [SerializeField, Range(0f, 1f)] private float _rareMobChance = 0.1f;

        [Header("Area Settings")] [SerializeField]
        private Vector2 _spawnRange = new(5f, 5f);

        [SerializeField] private List<Transform> _fixedSpawnPoints;

        private float _timer = 0;
        private float _nextSpawnTime;
        private List<GameObject> _spawnedMobs = new();

        private void Update()
        {
            if (!_active) return;

            if (_timer < 0)
            {
                SpawnMob();
                _timer = _spawnInterval;
            }

            _timer -= Time.deltaTime;
        }

        private void SpawnMob()
        {
            if (_mobsToSpawn.Count == 0) return;

            // Выбираем моба для спавна с учетом шансов
            var mobData = ChooseMobToSpawn();
            if (mobData == null) return;

            // Определяем позицию спавна
            var spawnPosition = GetSpawnPosition();

            // Создаем моба
            var newMob = Instantiate(mobData._mobPrefab, spawnPosition, Quaternion.identity);

            // Усиливаем редких мобов
            if (mobData._isRare) EnhanceMob(newMob);

            _spawnedMobs.Add(newMob);
        }

        private MobSpawnData ChooseMobToSpawn()
        {
            // Сначала проверяем шанс появления редкого моба
            if (Random.value <= _rareMobChance)
            {
                var rareMobs = _mobsToSpawn.FindAll(m => m._isRare);
                if (rareMobs.Count > 0)
                    return rareMobs[Random.Range(0, rareMobs.Count)];
            }

            // Если редкий моб не выпал, выбираем обычного с учетом шансов
            var availableMobs = new List<MobSpawnData>();
            var chances = new List<float>();

            foreach (var mob in _mobsToSpawn)
            {
                if (mob._isRare) continue;

                availableMobs.Add(mob);
                chances.Add(mob._spawnChance);
            }

            if (availableMobs.Count == 0) return null;

            var totalChance = chances.Sum();

            var randomPoint = Random.value * totalChance;

            for (var i = 0; i < availableMobs.Count; i++)
            {
                if (randomPoint < chances[i])
                {
                    return availableMobs[i];
                }

                randomPoint -= chances[i];
            }

            return availableMobs[0];
        }

        private Vector3 GetSpawnPosition()
        {
            switch (_spawnerType)
            {
                case SpawnerType.RandomPoints:
                    return transform.position + new Vector3(
                        Random.Range(-_spawnRange.x, _spawnRange.x),
                        0,
                        Random.Range(-_spawnRange.y, _spawnRange.y));

                case SpawnerType.FixedPoints:
                    if (_fixedSpawnPoints.Count == 0) return transform.position;
                    return _fixedSpawnPoints[Random.Range(0, _fixedSpawnPoints.Count)].position;

                case SpawnerType.Area:
                    return transform.position + new Vector3(
                        Random.Range(-_spawnRange.x, _spawnRange.x),
                        0,
                        Random.Range(-_spawnRange.y, _spawnRange.y));

                default:
                    return transform.position;
            }
        }

        private void EnhanceMob(GameObject mob)
        {
            // Можно добавить другие эффекты: увеличение размера, частицы и т.д.
            mob.transform.localScale *= 1.3f;

            // Добавляем эффекты для редкого моба
            var particles = mob.GetComponentInChildren<ParticleSystem>();
            if (particles == null)
            {
                var particlesObj = new GameObject("RareParticles");
                particlesObj.transform.SetParent(mob.transform);
                particlesObj.transform.localPosition = Vector3.zero;
                particles = particlesObj.AddComponent<ParticleSystem>();
                // Настройка частиц...
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) _active = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) _active = false;
        }
    }
}