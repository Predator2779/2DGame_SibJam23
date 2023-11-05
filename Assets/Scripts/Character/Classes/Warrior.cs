using UnityEngine;

namespace Scripts.Character.Classes
{
    public class Warrior : Person
    {
        public Weapon weapon;
        [Min(1)] public int attackDamage;

        public void Attack()
        {
            if (weapon != null)
            {
                MultiplyDamage(weapon);
                weapon.PrimaryAction();
            }
        }

        private void MultiplyDamage(Weapon weapon) => weapon.DamageFactor = attackDamage;
    }
}