using Scripts.Character.Classes;
using Scripts.Core.Global;
using UnityEngine;

namespace Scripts.Character.Player.Handlers
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        [Header("Components:")]
        [SerializeField] private Animator _anim;

        [SerializeField] private PlayerAudioHandler _audioHandler;

        private Classes.Person _player;
        private TurnHandler _turnHandler;
        private ItemHandler _itemHandler;

        private bool _isMoved;
        private float _verticalAxis;
        private float _horizontalAxis;

        private void OnValidate()
        {
            if (_gameState == null)
                _gameState = FindObjectOfType<GameState>();
        }

        private void Start()
        {
            _player = GetComponent<Classes.Person>();
            _turnHandler = GetComponent<TurnHandler>();
            _itemHandler = GetComponent<ItemHandler>();
        }

        private void Update()
        {
            if (_gameState.State != GameStates.Playing) return;

            SetAxes();
            CheckMoving();
            Jump();

            ItemPickOrPut();

            UseItem();

            _anim.SetFloat("SpeedHorizontal", Mathf.Abs(GetMovementVector().x));
            _anim.SetFloat("SpeedUp", GetMovementVector().y);
            _anim.SetFloat("SpeedDown", -GetMovementVector().y);
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

        private TurnHandler.playerSides GetLastPlayerSide() => _turnHandler.currentSide;

        private void CheckPlayerSide()
        {
            // if (_verticalAxis < 0)
            //     SetPlayerSide(TurnHandler.playerSides.Front);
            //
            // if (_verticalAxis > 0)
            //     SetPlayerSide(TurnHandler.playerSides.Back);

            if (_horizontalAxis < 0)
                SetPlayerSide(TurnHandler.playerSides.Left);

            if (_horizontalAxis > 0)
                SetPlayerSide(TurnHandler.playerSides.Right);
        }

        private void SetPlayerSide(TurnHandler.playerSides side) => _turnHandler.SetPlayerSide(side);
        
        private void SetAxes()
        {
            // _verticalAxis = InputFunctions.GetVerticalAxis();
            _verticalAxis = 0;
            _horizontalAxis = Input.GetAxis("Horizontal");
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _player.Jump();
        }

        private void ItemPickOrPut()
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (_itemHandler.IsHolded) PutItem();
                else PickUpItem();
            }
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
        
        private void PickUpItem()
        {
            _itemHandler.PickUpItem();
            SetPlayerSide(GetLastPlayerSide());
        }

        private void PutItem() => _itemHandler.PutItem();
    }
}