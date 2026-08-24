using UnityEngine;

public class TilePopulator : MonoBehaviour
{
    [Header("Spawn Points")]
    [Tooltip("Масив с точките (SpawnPoints), разположени върху този Tile")]
    [SerializeField] private Transform[] _spawnPoints;

    [Header("Spawnable Objects")]
    [SerializeField] private GameObject[] _decorations;
    [SerializeField] private GameObject[] _enemies;
    
    [Header("Spawn Probabilities (0.0 to 1.0)")]
    [Range(0f, 1f)] [SerializeField] private float _enemySpawnChance = 0.2f;

    private void Start()
    {
        PopulateTile();
    }

    private void PopulateTile()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0) return;

        foreach (Transform spawnPoint in _spawnPoints)
        {
            if (spawnPoint == null) continue;

            GameObject prefabToSpawn = SelectPrefabToSpawn();
            if (prefabToSpawn != null)
            {
                Vector3 spawnPosition = CalculateSpawnPosition(prefabToSpawn, spawnPoint.position);
                GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                spawnedObject.transform.SetParent(transform);
            }
        }
    }

    private GameObject SelectPrefabToSpawn()
    {
        bool shouldSpawnEnemy = Random.value <= _enemySpawnChance;

        if (shouldSpawnEnemy && _enemies != null && _enemies.Length > 0)
        {
            int randomIndex = Random.Range(0, _enemies.Length);
            return _enemies[randomIndex];
        }
        else if (_decorations != null && _decorations.Length > 0)
        {
            int randomIndex = Random.Range(0, _decorations.Length);
            return _decorations[randomIndex];
        }

        return null;
    }

    private Vector3 CalculateSpawnPosition(GameObject prefab, Vector3 basePosition)
    {
        Vector3 adjustedPosition = basePosition;

        SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float halfHeight = spriteRenderer.bounds.extents.y;
            adjustedPosition.y += halfHeight;
        }

        return adjustedPosition;
    }
}