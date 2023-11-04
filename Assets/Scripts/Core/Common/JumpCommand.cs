using UnityEngine;

namespace Scripts.Core.Common
{
    public class JumpCommand : Command
    {
        public JumpCommand(Rigidbody2D actor, Vector2 moveDirection, ForceMode2D mode)
        {
            _actor = actor;
            _moveDirection = moveDirection;
            _mode = mode;
        }

        private Rigidbody2D _actor;
        private Vector2 _moveDirection;
        private ForceMode2D _mode;

        public override void Execute()
        {
            _actor.AddForce(_moveDirection, _mode);
        }
    }
}