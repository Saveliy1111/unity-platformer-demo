using UnityEngine;

public class Weight : MonoBehaviour
{
    [Header("Weight Settings")]
    [SerializeField] private int _weightLevel = 1;

    [Range(0f, 1f)]
    [SerializeField] private float _knockbackResistance = 0f;
    
    public int WeightLevel => _weightLevel;
    public float KnockbackResistance => _knockbackResistance;
}
