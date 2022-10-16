using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new LevelData", menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    public Sprite LevelImage; 
    public int BuildIndex;
    public string Name;
    public List<int> AdditiveScenes = new List<int>();
    public bool IsCompleted;
}