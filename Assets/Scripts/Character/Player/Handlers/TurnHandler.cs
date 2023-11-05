using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [SerializeField] private ItemHandler _itemHandler;
    [SerializeField] private Transform _playerSprite;
    [SerializeField] private Transform _playerTrigger;

    public enum playerSides
    {
        Left,
        Right
    }

    public playerSides currentSide;

    private Scripts.Character.Classes.Person _player;

    private void Start()
    {
        _player = GetComponent<Scripts.Character.Classes.Person>();
        _itemHandler = GetComponent<ItemHandler>();

        SetPlayerSide(playerSides.Right);
    }

    public void SetPlayerSide(playerSides side)
    {
        currentSide = side;

        switch (currentSide)
        {
            case playerSides.Left:
                RotateObj(_playerSprite, 0);
                break;
            case playerSides.Right:
                RotateObj(_playerSprite, 180f);
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
    //             ItemLeftPos();
    //             _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosBack;
    //             break;
    //         case playerSides.Left:
    //             RotateObj(_playerSprite, 0);
    //             ItemLeftPos();
    //             _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosLeft;
    //             break;
    //         case playerSides.Right:
    //             RotateObj(_playerSprite, 180f);
    //             ItemRightPos();
    //             _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosRight;
    //             break;
    //         default:
    //             RotateObj(_playerSprite, 0);
    //             ItemRightPos();
    //             _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosFront;
    //             break;
    //     }
    // }

    // private void ItemLeftPos()
    // {
    //     if (CheckExistingItem(out Transform item))
    //     {
    //         RotateObj(item.transform, 0);
    //         item.transform.localPosition = GlobalConstants.ItemPositionLeft;
    //     }
    // }
    //
    // private void ItemRightPos()
    // {
    //     if (CheckExistingItem(out Transform item))
    //     {
    //         RotateObj(item.transform, 180f);
    //         item.transform.localPosition = GlobalConstants.ItemPositionRight;
    //     }
    // }
    //
    // private bool CheckExistingItem(out Transform item)
    // {
    //     if (_itemHandler.HoldedItem != null)
    //     {
    //         item = _itemHandler.HoldedItem.transform;
    //         return true;
    //     }
    //
    //     item = null;
    //     return false;
    // }
    
    private void RotateObj(Transform obj, float angle) => _player.RotateByAngle(obj, angle);
}