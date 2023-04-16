using UnityEngine;
using UnityEngine.Events;

namespace LOK1game
{
    public class Hitbox : Actor, IDamagable
    {
        public UnityEvent<Damage> OnHit;

        public void TakeDamage(Damage damage)
        {
            OnHit?.Invoke(damage);

            GetEnvironmentLogger().Push($"{this} damage taken!", this);
        }
    }
}