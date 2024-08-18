#if UNITY_EDITOR

using UnityEditor.SceneManagement;

#endif

using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK1game.World
{
    public class WorldComposer : MonoBehaviour
    {
        [SerializeField] private WorldComposerLevelsData _levelsData;

        private void Awake()
        {
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