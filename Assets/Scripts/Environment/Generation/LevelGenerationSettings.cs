using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelSettings", menuName = "Game Settings/Level Generation Settings")]
public class LevelGenerationSettings : ScriptableObject
{
    [Header("Prefabs - Ground & Platforms")]
    public GameObject GroundPrefab;
    public GameObject NormalPlatformPrefab;
    public GameObject FallingPlatformPrefab;
    public GameObject MovingPlatformPrefab;

    [Header("Level Dimensions")]
    [Tooltip("Общ брой сегменти (колони), които съставят нивото по оста X.")]
    public int NumberOfChunks = 10;
    public float ChunkWidth = 24f;

    [Header("Gaps & Heights")]
    public float MinGapWidth = 8f;
    public float MaxGapWidth = 24f;

    [Header("Terrain Generation (Perlin Noise)")]
    public float NoiseScale = 0.5f;
    public float HeightMultiplier = 50f;

    [Header("Platform Generation Probabilities")]
    [Range(0f, 1f)] public float NormalPlatformChance = 0.7f;
    [Range(0f, 1f)] public float FallingPlatformChance = 0.2f;
    [Range(0f, 1f)] public float MovingPlatformChance = 0.1f;
}