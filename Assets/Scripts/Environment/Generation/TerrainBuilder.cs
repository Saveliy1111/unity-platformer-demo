using System.Collections.Generic;
using UnityEngine;

public static class TerrainBuilder
{
    public static List<Vector2> BuildMacroTerrain(LevelGenerationSettings settings)
    {
        List<Vector2> chunkPositions = new List<Vector2>();
        
        float seedOffset = Random.Range(0f, 10000f);
        float currentX = 0f;
        for (int i = 0; i < settings.NumberOfChunks; i++)
        {
            float noiseValue = Mathf.PerlinNoise((i + seedOffset) * settings.NoiseScale, 0f);
            
            float currentY = (noiseValue * 2f - 1f) * settings.HeightMultiplier;

            chunkPositions.Add(new Vector2(currentX, currentY));

            float nextGap = Random.Range(settings.MinGapWidth, settings.MaxGapWidth);

            currentX += settings.ChunkWidth + nextGap;
        }

        return chunkPositions;
    }
}