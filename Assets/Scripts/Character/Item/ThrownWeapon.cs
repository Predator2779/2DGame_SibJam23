using System;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Item
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class ThrownWeapon : Weapon
    {
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_rigidbody.bodyType == RigidbodyType2D.Dynamic && other.gameObject.CompareTag("Player") &&
                other.TryGetComponent<HealthProcessor>(out var healthProcessor))
            {
                TakeDamage(new List<HealthProcessor> {healthProcessor});
            }

            if (other.gameObject.layer == 10)
            {
                _rigidbody.bodyType = RigidbodyType2D.Static;
            }
        }
    }
}