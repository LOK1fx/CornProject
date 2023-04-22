using UnityEngine;

public sealed class DestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DestroyImmediate(gameObject);

        Debug.LogWarning("There is an object that was destroyed at startup. Maybe it is not needed here?");
    }
}