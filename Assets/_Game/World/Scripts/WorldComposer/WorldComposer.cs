using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR

using UnityEditor.SceneManagement;

#endif

namespace LOK1game.World
{
    public class WorldComposer : MonoBehaviour
    {
        [SerializeField] private WorldComposerLevelsData _levelsData;

        private void Awake()
        {
#if UNITY_EDITOR

            Logger.Push($"MainScene in world: {_levelsData.MainScene.SceneName}", ELoggerGroup.CurrentWorld, this);

            //Removing preview scenes
            if (SceneManager.sceneCount > 1)
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    var scene = SceneManager.GetSceneAt(i);

                    Logger.Push($"Scene at {i}: {scene.name}", ELoggerGroup.BaseInfo, this);

                    if (scene.name != _levelsData.MainScene.SceneName)
                    {
                        //SceneManager.UnloadSceneAsync(scene.name, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                        EditorSceneManager.CloseScene(scene, true);
                    }
                }
            }

#endif

            LoadAllAdditionalLevels();
        }

        public void LoadAllAdditionalLevels()
        {
            foreach (var level in _levelsData.AdditionalScenes)
            {
                SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
            }
        }

#if UNITY_EDITOR

        [ContextMenu("Load additional scenes")]
        private void LoadAllAdditionalLevels_Editor()
        {
            foreach (var level in _levelsData.AdditionalScenes)
            {
                EditorSceneManager.OpenScene($"{Constants.Editor.SCENES_PATH}/" +
                    $"{_levelsData.MainScene.SceneName}/{level.SceneName}{Constants.Editor.ExtensionsNames.SCENE}", OpenSceneMode.Additive);
            }
        }

#endif
    }
}