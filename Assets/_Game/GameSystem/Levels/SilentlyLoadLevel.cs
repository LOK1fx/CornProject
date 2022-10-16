using UnityEngine.SceneManagement;

public static class SilentlyLoadLevel
{
    public static void LoadLevel(LevelData levelData)
    {
        foreach (var index in levelData.AdditiveScenes)
        {
            SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        }
    }
}
