using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

#if UNITY_EDITOR

using LOK1game.Editor;
using UnityEditor;

#endif

public enum EGameModeState : ushort
{
    Starting = 1,
    Started,
    Ending,
    Ended,
}

namespace LOK1game.Game
{
    /// <summary>
    /// Базовый игровой режим со всеми нужными полями для создания
    /// игрового режима
    /// </summary>
    [Serializable]
    public abstract class BaseGameMode : MonoBehaviour, IGameMode
    {
        public EGameModeState State { get; protected set; }
        public List<GameObject> GameModeSpawnedObjects { get; private set; }

        public GameObject UiPrefab => _uiPrefab;
        public GameObject CameraPrefab => _cameraPrefab;
        public GameObject PlayerPrefab => _playerPrefab;
        public PlayerController PlayerController => _playerController;

        [SerializeField] private GameObject _uiPrefab;
        [SerializeField] private GameObject _cameraPrefab;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private PlayerController _playerController;

        private bool _isGameModeObjectListInitialized;

        public abstract EGameModeId Id { get; }

        public abstract IEnumerator OnEnd();
        public abstract IEnumerator OnStart();
        
        /// <summary>
        /// Создает объект, который сразу будет привязан к этому игровому режиму.
        /// </summary>
        /// <param name="gameObject">Объект который будет создан(префаб)</param>
        /// <param name="prefix">Префикс перед названием объекта {prefix}{objectName}{postfix}</param>
        /// <param name="postfix">Постфикс после названия объекта {prefix}{objectName}{postfix}</param>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Созданый объект</returns>
        protected T SpawnGameModeObject<T>(T gameObject, string prefix = "", string postfix = "") where T : Object
        {
            return SpawnGameModeObject<T>(gameObject, gameObject.name, prefix, postfix);
        }

        protected T SpawnGameModeObject<T>(T gameObject, string objectName, string prefix = "", string postfix = "") where T: Object
        {
            var newGameObject = Instantiate(gameObject);

            newGameObject.name = $"{prefix}{objectName}{postfix}";
            
            RegisterGameModeObject(newGameObject);

            return newGameObject;
        }

        protected PlayerController CreatePlayerController(IPawn controlledPawn)
        {
            var playerController = Instantiate(PlayerController);

            playerController.name = $"[{nameof(PlayerController)}]";
            playerController.SetControlledPawn(controlledPawn);

            return playerController;
        }

        protected Vector3 GetRandomSpawnPointPosition(int actorId = -1)
        {
            return GetRandomSpawnPointPosition(true, actorId);
        }
        
        protected Vector3 GetRandomSpawnPointPosition(bool playerFlag, int actorId = -1) 
        {
#if UNITY_EDITOR

            if (actorId == -1)
            {
                if (EditorConfig.GetSpawnType() == ESpawnType.FromCameraPosition)
                {
                    var camera = SceneView.GetAllSceneCameras()[0];

                    Debug.Log($"spawning at {camera} | {camera.transform.position}");

                    return camera.transform.position;
                }
            }

#endif

            var spawnPoints = FindObjectsOfType<CharacterSpawnPoint>().ToList();

            if (spawnPoints.Count == 0)
                return Vector3.zero;

            if (playerFlag)
                spawnPoints.RemoveAll(point => point.AllowPlayer == false);
            
            if (spawnPoints.Count < 1)
                return Vector3.zero;
            
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            return spawnPoint.transform.position;
        }

        /// <summary>
        /// Привязывает объект к режиму
        /// </summary>
        /// <param name="gameObject">Объект</param>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Данный объект, прошедший привязку</returns>
        protected T RegisterGameModeObject<T>(T gameObject) where T: Object
        {
            if (!_isGameModeObjectListInitialized)
            {
                GameModeSpawnedObjects = new List<GameObject>();

                _isGameModeObjectListInitialized = true;
            }

            GameModeSpawnedObjects.Add(gameObject as GameObject);
            DontDestroyOnLoad(gameObject as GameObject);

            return gameObject;
        }

        /// <summary>
        /// Удаляет все привязанные к режиму объекты
        /// </summary>
        /// <returns></returns>
        protected IEnumerator DestroyAllGameModeObjects()
        {
            foreach (var obj in GameModeSpawnedObjects)
            {
                Destroy(obj as GameObject);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}