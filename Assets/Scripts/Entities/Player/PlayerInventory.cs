using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    private int _keysCollected = 0;
    
    [Header("Events")]
    public UnityEvent<int> OnKeysChanged;

    void Start()
    {
        OnKeysChanged?.Invoke(_keysCollected);
    }

    public void AddKey()
    {
        _keysCollected++;
        OnKeysChanged?.Invoke(_keysCollected);
        Debug.Log("Key picked up! Current keys: " + _keysCollected);
    }
}
