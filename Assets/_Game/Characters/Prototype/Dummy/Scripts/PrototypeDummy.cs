using UnityEngine;

namespace LOK1game.Prototype
{
    public class PrototypeDummy : Actor, IDamagable
    {
        private Vector3 _startSize;

        private void Awake()
        {
            _startSize = transform.localScale;
        }

        private void LateUpdate()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * 7f);
        }

        public void TakeDamage(Damage damage)
        {
            transform.localScale *= 1.2f;
        }
    }
}