using System.Linq;
using UnityEngine;

namespace LOK1game
{
    public abstract class Pawn : Actor, IPawn
    {
        public bool IsLocal
        {
            get
            {
                return photonView.IsMine;
            }
        }

        public EPlayerType PlayerType => playerType;

        [SerializeField] protected EPlayerType playerType;

        private Controller _controller;

        public Controller Controller => _controller;
        public abstract void OnInput(object sender);

        public virtual void OnPocces(Controller sender)
        {
            _controller = sender;
        }

        public virtual void OnUnpocces()
        {
            _controller = null;
        }

        public static Vector3 GetRandomSpawnPosition()
        {
            return GetRandomSpawnPosition(true);
        }

        public static Vector3 GetRandomSpawnPosition(bool playerFlag)
        {
            var spawnPoints = FindObjectsOfType<CharacterSpawnPoint>().ToList();

            if (playerFlag)
                spawnPoints.RemoveAll(point => point.AllowPlayer == false);
            
            if (spawnPoints.Count < 1)
                return Vector3.zero;
            
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            return spawnPoint.transform.position;
        }

#region Loggers

        public Logger GetPlayerLogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.Player);
        }

        public Logger GetEnemiesLogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.Enemies);
        }

        public Logger GetAILogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.AI);
        }

        #endregion
    }
}