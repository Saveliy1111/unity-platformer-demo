using System.Collections.Generic;
using UnityEngine;

public enum PlatformType
{
    Static,
    Falling,
    Moving
}

public struct PlatformData
{
    public Vector2 Position;
    public PlatformType Type;
}

public static class PlatformBuilder
{
    private const float PLATFORM_SPACING_X = 4f; 
    private const float VERTICAL_NOISE = 1.5f;
    private const float MAX_JUMP_HEIGHT = 1.8f; 

    public static List<PlatformData> BuildBridge(Vector2 startPoint, Vector2 endPoint, LevelGenerationSettings settings)
    {
        List<PlatformData> bridgePlatforms = new List<PlatformData>();

        int platformCount = CalculatePlatformCount(startPoint, endPoint);
        if (platformCount <= 0) return bridgePlatforms;

        float previousPlatformY = startPoint.y;

        for (int i = 1; i <= platformCount; i++)
        {
            float progress = (float)i / (platformCount + 1);

            Vector2 rawPosition = CalculateRawPosition(startPoint, endPoint, progress);
            
            float safeY = ClampJumpHeight(rawPosition.y, previousPlatformY);
            Vector2 finalPosition = new Vector2(rawPosition.x, safeY);

            previousPlatformY = finalPosition.y;

            PlatformType chosenType = DeterminePlatformType(settings);

            bridgePlatforms.Add(new PlatformData
            {
                Position = finalPosition,
                Type = chosenType
            });
        }

        return bridgePlatforms;
    }

    private static int CalculatePlatformCount(Vector2 startPoint, Vector2 endPoint)
    {
        float gapWidth = endPoint.x - startPoint.x;
        return Mathf.FloorToInt(gapWidth / PLATFORM_SPACING_X);
    }

    private static Vector2 CalculateRawPosition(Vector2 startPoint, Vector2 endPoint, float progress)
    {
        float lerpedX = Mathf.Lerp(startPoint.x, endPoint.x, progress);
        float lerpedY = Mathf.Lerp(startPoint.y, endPoint.y, progress);

        float noiseY = Random.Range(-VERTICAL_NOISE, VERTICAL_NOISE);
        
        return new Vector2(lerpedX, lerpedY + noiseY);
    }

    private static float ClampJumpHeight(float targetY, float previousY)
    {
        if (targetY > previousY + MAX_JUMP_HEIGHT)
        {
            return previousY + MAX_JUMP_HEIGHT;
        }
        
        return targetY;
    }

    private static PlatformType DeterminePlatformType(LevelGenerationSettings settings)
    {
        float randomValue = Random.value; 

        if (randomValue <= settings.MovingPlatformChance)
        {
            return PlatformType.Moving;
        }
        
        if (randomValue <= settings.MovingPlatformChance + settings.FallingPlatformChance)
        {
            return PlatformType.Falling;
        }
        
        return PlatformType.Static;
    }
}