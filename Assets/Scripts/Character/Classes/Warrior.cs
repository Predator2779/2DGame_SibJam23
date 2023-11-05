using UnityEngine;

namespace Scripts.Character.Classes
{
    public class Warrior : Person
    {
        public Weapon weapon;
        [Min(1)] public int attackDamage;
        [SerializeField] private HealthTrigger _healthTrigger;
        
        public void Attack()
        {
            if (weapon != null)
            {
                MultiplyDamage(weapon);
                weapon.TakeDamage(_healthTrigger._healthProcessors);
            }
        }

        private void MultiplyDamage(Weapon weapon) => weapon.DamageFactor = attackDamage;
    }
}