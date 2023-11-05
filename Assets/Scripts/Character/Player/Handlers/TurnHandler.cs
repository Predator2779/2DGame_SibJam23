using Scripts.Core.Common;
using UnityEngine;

public class TurnHandler
{
    public enum playerSides
    {
        Left,
        Right
    }

    public playerSides currentSide;

    public void SetCharacterSprite(playerSides side, Transform sprite)
    {
        currentSide = side;

        switch (currentSide)
        {
            case playerSides.Left:
                RotateObj(sprite, 0);
                break;
            default:
                RotateObj(sprite, 180f); ;
                break;
        }
    }

    private void RotateObj(Transform obj, float angle) => new RotationCommand(obj).ExecuteByValue(angle);
}