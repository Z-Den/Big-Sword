using System;
using System.Collections.Generic;
using System.Linq;
using BigSword.Scripts.ScoreSystem;
using Units;
using Units.Enemy;
using UnityEngine;
using UnityEngine.Serialization;
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
        public Enemy _mobPrefab;
        [Range(0f, 1f)] public float _spawnChance = 0.5f;
        [Min(0)] public int _minLevelToSpawn = 0;
        [Min(0)] public int _maxLevelToSpawn = 0;
        public bool _isRare = false;
    }

    public class MobSpawner : MonoBehaviour
    {
        public static MobSpawner Instance { get; private set; }
        private SpawnerType _spawnerType;

        private int _currentLevel = 0;
        private float _spawnInterval = 5f;
        private List<MobSpawnData> _mobsToSpawn;
        private float _rareMobChance = 0.1f;
        private Vector2 _spawnRange = new(5f, 5f);

        private List<Transform> _fixedSpawnPoints = new List<Transform>();

        private float _timer = 0;
        private float _nextSpawnTime;
        
        
        private List<MobSpawnData> _preferredMobs => 
            _mobsToSpawn.Where(x => _currentLevel >= x._minLevelToSpawn && _currentLevel <= x._maxLevelToSpawn).ToList();
        public Action<Enemy> OnSpawn;

        public void Init(SpawnerType type, List<MobSpawnData> mobsToSpawn)
        {
            _mobsToSpawn = mobsToSpawn;
            _spawnerType = type;
            Instance = FindAnyObjectByType<MobSpawner>();
        }

        private void Start()
        {
            ScoreSystem.Instance.OnNewLevelReached += OnNewLevelReached;
            _currentLevel = ScoreSystem.Instance.Level;
        }

        private void OnNewLevelReached(int level)
        {
            _currentLevel = level;
        }

        private void Update()
        {
            if (_fixedSpawnPoints.Count == 0)
                return;
            
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
            if (mobData._isRare) EnhanceMob(newMob.gameObject);
            
            OnSpawn?.Invoke(newMob);
        }

        private MobSpawnData ChooseMobToSpawn()
        {
            // Сначала проверяем шанс появления редкого моба
            if (Random.value <= _rareMobChance)
            {
                var rareMobs = _preferredMobs.FindAll(m => m._isRare);
                if (rareMobs.Count > 0)
                    return rareMobs[Random.Range(0, rareMobs.Count)];
            }

            // Если редкий моб не выпал, выбираем обычного с учетом шансов
            var availableMobs = new List<MobSpawnData>();
            var chances = new List<float>();

            foreach (var mob in _preferredMobs)
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

        public void PlayerOnTrigger(SpawnTrigger spawnTrigger)
        {
            _fixedSpawnPoints.AddRange(spawnTrigger.SpawnPoints);
        }

        public void PlayerFromTrigger(SpawnTrigger spawnTrigger)
        {
            foreach (var spawnPoint in spawnTrigger.SpawnPoints) 
                _fixedSpawnPoints.Remove(spawnPoint);
        }
    }
}