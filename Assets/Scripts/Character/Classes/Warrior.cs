using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Character.Classes
{
    public class Warrior : Person
    {
        public Weapon currentWeapon;
        [Min(1)] public int attackDamage;
        public WeaponTrigger weaponTrigger;
        
        [SerializeField] private int _weaponIndex;
        [SerializeField] private List<Weapon> _weapons = new();

        public void Attack()
        {
            if (currentWeapon != null)
            {
                MultiplyDamage(currentWeapon);
                currentWeapon.TakeDamage(weaponTrigger._healthProcessors);
            }
        }

        public Weapon ChangeWeapon()
        {
            if (_weapons.Count > 0)
            {
                int nextIndex = _weaponIndex + 1;
            
                if (nextIndex >= _weapons.Count) nextIndex = 0;
                
                currentWeapon = _weapons[nextIndex];
                _weaponIndex = nextIndex;

                return currentWeapon;
            }

            return null;
        }
        
        public void AddWeapon(Weapon weapon)
        {
            if (!_weapons.Contains(weapon))
            {
                _weapons.Add(weapon);
                weapon.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
        
        private void MultiplyDamage(Weapon weapon) => weapon.DamageFactor = attackDamage;
    }
}