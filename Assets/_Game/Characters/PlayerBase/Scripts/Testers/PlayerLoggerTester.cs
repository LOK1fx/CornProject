using UnityEngine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerLoggerTester : MonoBehaviour
    {
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();

            _player.OnDeath += OnPlayerDeath;
            _player.Movement.OnStartCrouch += OnPlayerStartedCrouching;
        }

        private void OnDestroy()
        {
            _player.OnDeath -= OnPlayerDeath;
            _player.Movement.OnStartCrouch -= OnPlayerStartedCrouching;
        }

        private void OnPlayerStartedCrouching()
        {
            Logger.Push("Player started crouching", ELoggerGroup.Player, this);
        }

        private void OnPlayerDeath()
        {
            Logger.Push("Player dead", ELoggerGroup.Player, this);
        }
    }
}
