using UnityEngine;
using UnityEngine.Events;

namespace LOK1game
{
    public class Hitbox : MonoBehaviour, IDamagable
    {
        public UnityEvent<Damage> OnHit;

        public void TakeDamage(Damage damage)
        {
            OnHit?.Invoke(damage);
        }
    }
}