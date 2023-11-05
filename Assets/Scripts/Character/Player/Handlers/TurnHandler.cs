using Scripts.Core.Global;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [SerializeField] private Collider2D _weaponTrigger;
    [SerializeField] private Transform _playerSprite;

    public enum playerSides
    {
        // Front,
        // Back,
        Left,
        Right
    }

    public playerSides currentSide;

    private Scripts.Character.Classes.Person _player;

    private void Start()
    {
        _player = GetComponent<Scripts.Character.Classes.Person>();

        SetPlayerSide(playerSides.Right);
    }

    public void SetPlayerSide(playerSides side)
    {
        currentSide = side;

        switch (currentSide)
        {
            case playerSides.Left:
                RotateObj(_playerSprite, 0);
                SetWeaponPosition(GlobalConstants.PlayerTriggerPosLeft);
                break;
            default:
                RotateObj(_playerSprite, 180f);
                SetWeaponPosition(GlobalConstants.PlayerTriggerPosRight);
                break;
        }
    }
    
    // public void SetPlayerSide(playerSides side)
    // {
    //     currentSide = side;
    //
    //     switch (currentSide)
    //     {
    //         case playerSides.Back:
    //             RotateObj(_playerSprite, 0);
    //             SetWeaponPosition(GlobalConstants.WeaponPositionLeft);
    //             _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosBack;
    //             break;
    //         case playerSides.Left:
    //             RotateObj(_playerSprite, 0);
    //             SetWeaponPosition(GlobalConstants.WeaponPositionLeft);
    //             _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosLeft;
    //             break;
    //         case playerSides.Right:
    //             RotateObj(_playerSprite, 180f);
    //             SetWeaponPosition(GlobalConstants.WeaponPositionRight);
    //             _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosRight;
    //             break;
    //         default:
    //             RotateObj(_playerSprite, 0);
    //             SetWeaponPosition(GlobalConstants.WeaponPositionRight);
    //             _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosFront;
    //             break;
    //     }
    // }

    private void SetWeaponPosition(Vector2 position) => _weaponTrigger.transform.localPosition = position;
    private void RotateObj(Transform obj, float angle) => _player.RotateByAngle(obj, angle);
}