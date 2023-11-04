using UnityEngine;
using UnityEngine.Events;

namespace LOK1game
{
    public class Health : MonoBehaviour
    {
        public UnityAction<int> OnHealthChanged;

        public int MaxHp { get; private set; }
        public int Hp { get; private set; }

        [SerializeField] private int _defaultHp = 100;

        private void Awake()
        {
            MaxHp = _defaultHp;
            Hp = _defaultHp;
        }

        public void ResetHealth()
        {
            Hp = MaxHp;

            SetAccurateHealth();

            OnHealthChanged?.Invoke(Hp);
        }

        public void SetHealth(int value)
        {
            Hp = value;

            SetAccurateHealth();

            OnHealthChanged?.Invoke(Hp);
        }

        public void ReduceHealth(int value)
        {
            Hp -= value;

            SetAccurateHealth();

            OnHealthChanged?.Invoke(Hp);
        }

        public void AddHealth(int value)
        {
            Hp += value;

            SetAccurateHealth();

            OnHealthChanged?.Invoke(Hp);
        }

        private void SetAccurateHealth()
        {
            Hp = Mathf.Min(Hp, MaxHp);

            if (Hp < 0)
                Hp = 0;
        }
    }
}