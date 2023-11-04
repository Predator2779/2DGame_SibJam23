using UnityEngine;

namespace Scripts.Core.Common
{
    public class MoveCommand : Command
    {
        public MoveCommand(Rigidbody2D actor, Vector2 moveDirection, float speed)
        {
            _actor = actor;
            _moveDirection = moveDirection;
            _speed = speed;
        }

        private Rigidbody2D _actor;
        private Vector2 _moveDirection;
        private float _speed;

        public override void Execute()
        {
            if (_actor.velocity.x > -_speed && _actor.velocity.x < _speed)
                _actor.velocity = new Vector2(_moveDirection.x * _speed, _actor.velocity.y);
        }
    }
}