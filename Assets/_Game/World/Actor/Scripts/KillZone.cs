using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Collider))]
    public class KillZone : Actor
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerDomain.Player>(out var player))
            {
                if (player.IsLocal)
                    player.TakeDamage(new Damage(player.Health.MaxHp));
            }
        }
    }
}