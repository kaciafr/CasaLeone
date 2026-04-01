using UnityEngine;

[System.Serializable]
public class ClientEntry
{
    public GameObject prefab;
    [Range(0f, 1f)] public float weight = 1f;
}