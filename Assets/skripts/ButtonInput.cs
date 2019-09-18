using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInput : MonoBehaviour
{
    public enum Direction
    {
        left,
        right
    }

    public Direction direction;

    public delegate void ButtonPressed();
    public static event ButtonPressed OnLeft;
    public static event ButtonPressed OnRight;


    private void OnMouseDown()
    {
        if (OnLeft != null && direction == Direction.left)
        {
            OnLeft();
        }
        else if (OnRight != null && direction == Direction.right)
        {
            OnRight();
        }
    }
}
