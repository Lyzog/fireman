using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiremanController : MonoBehaviour
{
    public List<Transform> positions = new List<Transform>();

    private int currentPosition = 1;

    private void OnEnable()
    {
        ButtonInput.OnLeft += MoveLeft;
        ButtonInput.OnRight += MoveRight;
    }

    private void OnDisable()
    {
        ButtonInput.OnLeft -= MoveLeft;
        ButtonInput.OnRight -= MoveRight;
    }

    private void Start()
    {
        UpdatePosition();
    }

    private void MoveRight()
    {
        if (currentPosition < positions.Count - 1)
        {
            currentPosition++;
            UpdatePosition();
        }
    }

    private void MoveLeft()
    {
        if (currentPosition > 0)
        {
            currentPosition--;
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        transform.position = positions[currentPosition].position;
    }


}
