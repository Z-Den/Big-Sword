using UnityEngine;
using System.Collections.Generic;

public enum SpawnerType
{
    RandomPoints,    // Случайные точки на карте
    FixedPoints,     // Заранее заданные точки
    Area            // В пределах заданной области
}

[System.Serializable]
public class MobSpawnData
{
    public GameObject mobPrefab;
    [Range(0f, 1f)] public float spawnChance = 0.5f;
    public bool isRare = false;
    public int minLevel = 1;
    public int maxLevel = 10;
}

public class MobSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public SpawnerType spawnerType;
    public float spawnInterval = 5f;
    public int maxMobs = 10;
    public bool active = false;
    
    [Header("Mob Settings")]
    public List<MobSpawnData> mobsToSpawn;
    [Range(0f, 1f)] public float rareMobChance = 0.1f;
    
    [Header("Area Settings")]
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);
    public List<Transform> fixedSpawnPoints;
    
    private float nextSpawnTime;
    private List<GameObject> spawnedMobs = new List<GameObject>();
    
    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }
    
    void Update()
    {
        if (!active) return;

        if (Time.time >= nextSpawnTime && spawnedMobs.Count < maxMobs)
        {
            SpawnMob();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }
    
    void SpawnMob()
    {
        if (mobsToSpawn.Count == 0) return;
        
        // Выбираем моба для спавна с учетом шансов
        MobSpawnData mobData = ChooseMobToSpawn();
        if (mobData == null) return;
        
        // Определяем позицию спавна
        Vector3 spawnPosition = GetSpawnPosition();
        
        // Создаем моба
        GameObject newMob = Instantiate(mobData.mobPrefab, spawnPosition, Quaternion.identity);
        
        // Усиливаем редких мобов
        if (mobData.isRare)
        {
            EnhanceMob(newMob);
        }
        
        spawnedMobs.Add(newMob);
    }
    
    MobSpawnData ChooseMobToSpawn()
    {
        // Сначала проверяем шанс появления редкого моба
        if (Random.value <= rareMobChance)
        {
            List<MobSpawnData> rareMobs = mobsToSpawn.FindAll(m => m.isRare);
            if (rareMobs.Count > 0)
            {
                return rareMobs[Random.Range(0, rareMobs.Count)];
            }
        }
        
        // Если редкий моб не выпал, выбираем обычного с учетом шансов
        List<MobSpawnData> availableMobs = new List<MobSpawnData>();
        List<float> chances = new List<float>();
        
        foreach (var mob in mobsToSpawn)
        {
            if (!mob.isRare)
            {
                availableMobs.Add(mob);
                chances.Add(mob.spawnChance);
            }
        }
        
        if (availableMobs.Count == 0) return null;
        
        float totalChance = 0f;
        foreach (float chance in chances) totalChance += chance;
        
        float randomPoint = Random.value * totalChance;
        
        for (int i = 0; i < availableMobs.Count; i++)
        {
            if (randomPoint < chances[i])
            {
                return availableMobs[i];
            }
            randomPoint -= chances[i];
        }
        
        return availableMobs[0];
    }
    
    Vector3 GetSpawnPosition()
    {
        switch (spawnerType)
        {
            case SpawnerType.RandomPoints:
                return transform.position + new Vector3(
                    Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                    0,
                    Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2));
                
            case SpawnerType.FixedPoints:
                if (fixedSpawnPoints.Count == 0) return transform.position;
                return fixedSpawnPoints[Random.Range(0, fixedSpawnPoints.Count)].position;
                
            case SpawnerType.Area:
                return transform.position + new Vector3(
                    Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                    0,
                    Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2));
                    
            default:
                return transform.position;
        }
    }
    
    void EnhanceMob(GameObject mob)
    {
        // Пример усиления моба - можно добавить свои компоненты
        //var health = mob.GetComponent<Health>();
        // if (health != null)
        // {
        //     health.maxHealth *= 2f;
        //     health.currentHealth = health.maxHealth;
        // }
        
        var renderer = mob.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red; // Визуальное выделение редкого моба
        }
        
        // Можно добавить другие эффекты: увеличение размера, частицы и т.д.
        mob.transform.localScale *= 1.3f;
        
        // Добавляем эффекты для редкого моба
        var particles = mob.GetComponentInChildren<ParticleSystem>();
        if (particles == null)
        {
            GameObject particlesObj = new GameObject("RareParticles");
            particlesObj.transform.SetParent(mob.transform);
            particlesObj.transform.localPosition = Vector3.zero;
            particles = particlesObj.AddComponent<ParticleSystem>();
            // Настройка частиц...
        }
    }
    
    void OnDrawGizmos()
    {
        if (spawnerType == SpawnerType.RandomPoints || spawnerType == SpawnerType.Area)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y));
        }
        
        if (spawnerType == SpawnerType.FixedPoints)
        {
            Gizmos.color = Color.blue;
            foreach (var point in fixedSpawnPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawSphere(point.position, 0.5f);
                    Gizmos.DrawLine(transform.position, point.position);
                }
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            active = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            active = false;
        }
    }
}