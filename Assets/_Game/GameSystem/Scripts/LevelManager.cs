using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelManager
{
    public static List<LevelData> LevelsData { get; private set; }

    [SerializeField] private List<LevelData> _levelsData = new List<LevelData>();

    public void Initialize()
    {
        LevelsData = _levelsData;

#if UNITY_EDITOR
        foreach (var data in LevelsData)
        {
            data.IsCompleted = false;
        }
#endif
    }

    public static void LoadLevel(LevelData data)
    {
        SceneManager.LoadSceneAsync(data.BuildIndex, LoadSceneMode.Single);
    }

    public static void LoadNextLevel()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadSceneAsync(currentScene + 1);
    }

    public static void SetLevelCompleted(string name)
    {
        foreach (var data in LevelsData)
        {
            if (data.Name == name)
            {
                data.IsCompleted = true;
            }
        }
    }

    public static LevelData GetCurrentLevelData()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        foreach (var level in LevelsData)
        {
            if(level.BuildIndex == currentSceneIndex)
            {
                return level;
            }
        }

        Debug.LogError($"LevelManager can't find a level with the build index {currentSceneIndex}.");

        return null;
    }

    public static LevelData GetLevelData(int buildIndex)
    {
        foreach (var data in LevelsData)
        {
            if(data.BuildIndex == buildIndex)
            {
                var dataInstance = ScriptableObject.CreateInstance<LevelData>();

                dataInstance = data;

                return dataInstance;
            }
        }

        return null;
    }

    public static LevelData GetLevelData(string name)
    {
        foreach (var data in LevelsData)
        {
            if (data.Name == name)
            {
                var dataInstance = ScriptableObject.CreateInstance<LevelData>();

                dataInstance = data;

                return dataInstance;
            }
        }

        return null;
    }
}
