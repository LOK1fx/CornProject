using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.World
{
    [CreateAssetMenu(fileName = "New World Levels", menuName = "World/Composer/Levels")]
    public class WorldComposerLevelsData : ScriptableObject
    {
        public SceneField MainScene => _mainScene;
        public List<SceneField> AdditionalScenes => _additionalScenes;

        [SerializeField] private SceneField _mainScene;
        [SerializeField] private List<SceneField> _additionalScenes;
    }
}