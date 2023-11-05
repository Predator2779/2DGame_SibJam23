using Character.Item;
using UnityEngine;

namespace Scripts.Character.Classes
{
    public class Archer : Person
    {
        [SerializeField] private Arrow arrowPrefab;

        public void Shoot()
        {
            var rotation = transform.rotation;
            var arrow = Instantiate(arrowPrefab, transform.position + Vector3.up * 2 + rotation * Vector3.right * 7,
                rotation * Quaternion.Euler(0, 180, 0));
            arrow.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.right * arrow.speed;
        }
    }
}