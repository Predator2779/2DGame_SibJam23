using System;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Item
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Arrow : Weapon
    {
        [SerializeField] public float speed;
        [SerializeField] private float lifeDuration;

        private float _spawnTimestamp;

        protected override void Start()
        {
            base.Start();

            _destroyAfterUsing = true;
            _spawnTimestamp = Time.time;
        }

        private void Update()
        {
            if (Time.time - _spawnTimestamp > lifeDuration)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") &&
                other.TryGetComponent<HealthProcessor>(out var healthProcessor))
            {
                TakeDamage(new List<HealthProcessor> {healthProcessor});
            }
        }
    }
}