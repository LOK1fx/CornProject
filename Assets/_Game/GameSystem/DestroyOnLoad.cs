using UnityEngine;

public sealed class DestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DestroyImmediate(gameObject);

        Debug.LogWarning("There is an object that is destroyed at startup, maybe it is not needed here?");
    }
}