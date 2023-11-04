using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Other
{
    public class Following : MonoBehaviour
    {
        [SerializeField] private Transform _trackingObject;
        [SerializeField] private float _distance;
        [SerializeField] private float _speed;

        private Vector3 _offset;
        private bool _isMove;

        private void Start()
        {
            _offset = transform.position;
        }

        private void FixedUpdate()
        {
            Checking();
            Moving();
        }

        private void Checking()
        {
            if (transform.position.x - _trackingObject.position.x > _distance ||
                _trackingObject.position.x - transform.position.x > _distance ||
                transform.position.y - _trackingObject.position.y > _distance ||
                _trackingObject.position.y - transform.position.y > _distance)
                _isMove = true;
        }

        private void Moving()
        {
            if (transform.position != _trackingObject.position + _offset && _isMove)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                        _trackingObject.position + _offset, _speed * Time.deltaTime);

                return;
            }

            _isMove = false;
        }
    }
}