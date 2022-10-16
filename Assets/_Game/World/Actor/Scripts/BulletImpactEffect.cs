using UnityEngine;

namespace LOK1game.World
{
    public class BulletImpactEffect : MonoBehaviour, IDamagable
    {
        [SerializeField] private GameObject _bulletHolePrefab;

        public void TakeDamage(Damage damage)
        {
            if(_bulletHolePrefab == null) { return; }

            var offset = 0.001f;
            var bulletHole = Instantiate(_bulletHolePrefab, damage.HitPoint + damage.HitNormal * offset, Quaternion.identity);

            bulletHole.transform.LookAt(damage.HitPoint + damage.HitNormal);

            Destroy(bulletHole, 4f);
        }
    }
}