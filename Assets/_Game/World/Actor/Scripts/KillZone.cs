using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Collider))]
    public class KillZone : Actor
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player.Player>(out var player))
            {
                player.TakeDamage(new Damage(player.Health.MaxHp));
            }
        }
    }
}