using UnityEngine;

namespace Scripts.Other
{
    public class Following : MonoBehaviour
    {
        [SerializeField] private Transform _trackingObject;
        [SerializeField] private float _speed;

        private void FixedUpdate()
        {
            transform.position = Vector2.MoveTowards(transform.position,
                    GetVector(), _speed * Time.deltaTime);
        }

        private Vector2 GetVector() => new(_trackingObject.position.x, transform.position.y);
    }
}