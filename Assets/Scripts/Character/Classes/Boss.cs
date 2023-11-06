using System;
using Character.Item;
using UnityEngine;

namespace Scripts.Character.Classes
{
    public class Boss : Person
    {
        [SerializeField] public Weapon heldWeapon;
        [SerializeField] [Min(1)] private int attackDamage;
        [SerializeField] private WeaponTrigger weaponTrigger;

        [SerializeField] private ThrownWeapon thrownWeaponPrefab;
        [SerializeField] private float thrownWeaponSpeedH;
        [SerializeField] private float thrownWeaponSpeedV;

        public void MeleeAttack()
        {
            if (heldWeapon.gameObject.activeSelf)
            {
                MultiplyDamage(heldWeapon);
                heldWeapon.TakeDamage(weaponTrigger._healthProcessors);
            }
        }

        public ThrownWeapon RangedAttack()
        {
            var rotation = heldWeapon.transform.rotation;
            var thrownWeapon = Instantiate(thrownWeaponPrefab, heldWeapon.transform.position, rotation);
            var weaponRigidbody = thrownWeapon.GetComponent<Rigidbody2D>();
            weaponRigidbody.velocity = rotation * Vector3.right * thrownWeaponSpeedH + Vector3.up * thrownWeaponSpeedV;
            weaponRigidbody.angularVelocity = Math.Abs((rotation.eulerAngles.y % 360) - 180) < 90 ? 60 : -720;
            heldWeapon.gameObject.SetActive(false);
            return thrownWeapon;
        }

        private void MultiplyDamage(Weapon weapon) => weapon.DamageFactor = attackDamage;

        public override void Death()
        {
            
        }
    }
}