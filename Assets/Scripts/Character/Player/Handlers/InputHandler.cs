using Scripts.Character.Classes;
using Scripts.Core.Global;
using UnityEngine;

namespace Scripts.Character.Player.Handlers
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;
        [SerializeField] private Animator _anim;
        [SerializeField] private PlayerAudioHandler _audioHandler;

        private Person _player;
        private PickItem _pickItem;

        private bool _isMoved;
        private float _verticalAxis;
        private float _horizontalAxis;

        private void OnValidate()
        {
            if (_gameState == null) _gameState = FindObjectOfType<GameState>();
        }

        private void Awake()
        {
            _player = GetComponent<Person>();
            _pickItem = GetComponent<PickItem>();
        }

        private void Update()
        {
            if (_gameState.State != GameStates.Playing) return;

            SetAxes();
            CheckMoving();
            Jump();

            CheckPress_E();
            CheckPress_C();

            UseItem();
            UseWeapon();

            // _anim.SetFloat("SpeedHorizontal", Mathf.Abs(GetMovementVector().x));
            // _anim.SetFloat("SpeedUp", GetMovementVector().y);
            // _anim.SetFloat("SpeedDown", -GetMovementVector().y);
        }

        private void FixedUpdate()
        {
            if (_gameState.State != GameStates.Playing) return;

            if (_isMoved)
            {
                _player.MoveTo(GetMovementVector());
                CheckPlayerSide();
                _audioHandler.TakeStep();
            }
            else
            {
                _player.StopMove();
                _audioHandler.StopPlaying();
            }
        }

        private void CheckMoving()
        {
            if (Input.GetKeyDown(KeyCode.Space) || _verticalAxis != 0 || _horizontalAxis != 0)
                _isMoved = true;
            else _isMoved = false;
        }

        private Vector2 GetMovementVector()
        {
            var v = _player.transform.up * _verticalAxis;
            var h = _player.transform.right * _horizontalAxis;

            Vector2 vector = h + v;

            return vector;
        }
        
        private void CheckPlayerSide()
        {
            if (_horizontalAxis < 0)
                _player.SetSpriteSide(TurnHandler.playerSides.Left);

            if (_horizontalAxis > 0)
                _player.SetSpriteSide(TurnHandler.playerSides.Right);
        }

        private void SetAxes()
        {
            _verticalAxis = 0;
            _horizontalAxis = Input.GetAxis("Horizontal");
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.W)) _player.Jump();
        }

        private void CheckPress_E()
        {
            if (Input.GetKeyUp(KeyCode.E)) _pickItem.Pressed_E();
        }

        private void CheckPress_C()
        {
            if (Input.GetKeyUp(KeyCode.C)) _pickItem.Pressed_C();
        }

        private void UseItem()
        {
            if (Input.GetKeyUp(KeyCode.F)) _player.UseItem();
        }

        private void UseWeapon()
        {
            if (Input.GetMouseButtonUp(0) && _player.TryGetComponent(out Warrior warrior))
                warrior.Attack();
        }
    }
}