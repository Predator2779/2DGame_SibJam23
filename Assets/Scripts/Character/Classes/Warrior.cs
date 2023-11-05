using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Character.Classes
{
    public class Warrior : Person
    {
        public Weapon _currentWeapon;
        [Min(1)] public int attackDamage;
        [SerializeField] private HealthTrigger _healthTrigger;

        [SerializeField] private int _weaponIndex;
        [SerializeField] private List<Weapon> _weapons = new();
        
        public void Attack()
        {
            if (_currentWeapon != null)
            {
                MultiplyDamage(_currentWeapon);
                _currentWeapon.TakeDamage(_healthTrigger._healthProcessors);
            }
        }

        public Weapon ChangeWeapon()
        {
            if (_weapons.Count > 0)
            {
                int nextIndex = _weaponIndex + 1;
            
                if (nextIndex >= _weapons.Count) nextIndex = 0;
                
                _currentWeapon = _weapons[nextIndex];
                _weaponIndex = nextIndex;

                return _currentWeapon;
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