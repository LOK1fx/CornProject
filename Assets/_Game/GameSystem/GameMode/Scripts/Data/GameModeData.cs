using UnityEngine;

namespace LOK1game.Game
{
    [CreateAssetMenu()]
    public class GameModeData : ScriptableObject
    {
        public EGameModeId Id => _id;

        public GameObject UiPrefab => _uiPrefab;
        public GameObject CameraPrefab => _cameraPrefab;
        public GameObject PlayerPrefab => _playerPrefab;
        public GameObject PlayerController => _playerController;

        [SerializeField] private EGameModeId _id = EGameModeId.None;
        [SerializeField] private GameObject _uiPrefab;
        [SerializeField] private GameObject _cameraPrefab;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _playerController;
    }
}
