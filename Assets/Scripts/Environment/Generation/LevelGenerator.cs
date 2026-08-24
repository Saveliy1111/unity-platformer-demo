using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private LevelGenerationSettings _settings;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        if (_settings == null) return;

        Transform levelContainer = new GameObject("GeneratedLevel").transform;

        // --- ФАЗА 1: Макро-генериране (Големи Ground масиви) ---
        List<Vector2> chunkPositions = TerrainBuilder.BuildMacroTerrain(_settings);
        SpawnMacroTerrain(chunkPositions, levelContainer);

        // --- ФАЗА 2: Микро-генериране (Платформи в пропастите) ---
        SpawnPlatforms(chunkPositions, levelContainer);

        Debug.Log("--- Генерирането приключи успешно ---");
    }

    private void SpawnMacroTerrain(List<Vector2> chunkPositions, Transform container)
    {
        foreach (Vector2 pos in chunkPositions)
        {
            Vector2 spawnPos = new Vector3(pos.x, pos.y);
            GameObject groundTile = Instantiate(_settings.GroundPrefab, spawnPos, Quaternion.identity);
            groundTile.transform.SetParent(container);
        }
    }

    private void SpawnPlatforms(List<Vector2> chunkPositions, Transform container)
    {
        for (int i = 0; i < chunkPositions.Count - 1; i++)
        {
            float currentChunkRightEdge = chunkPositions[i].x + (_settings.ChunkWidth / 2f);
            float nextChunkLeftEdge = chunkPositions[i + 1].x - (_settings.ChunkWidth / 2f);

            Vector2 gapStart = new Vector2(currentChunkRightEdge, chunkPositions[i].y);
            Vector2 gapEnd = new Vector2(nextChunkLeftEdge, chunkPositions[i + 1].y);

            List<PlatformData> bridges = PlatformBuilder.BuildBridge(gapStart, gapEnd, _settings);
            foreach (PlatformData platformData in bridges)
            {
                GameObject prefabToSpawn = GetPrefabByType(platformData.Type);
                
                if (prefabToSpawn != null)
                {
                    GameObject platform = Instantiate(prefabToSpawn, platformData.Position, Quaternion.identity);
                    platform.transform.SetParent(container);
                }
            }
        }
    }

    private GameObject GetPrefabByType(PlatformType type)
    {
        switch (type)
        {
            case PlatformType.Falling: return _settings.FallingPlatformPrefab;
            case PlatformType.Moving: return _settings.MovingPlatformPrefab;
            default: return _settings.NormalPlatformPrefab;
        }
    }
}