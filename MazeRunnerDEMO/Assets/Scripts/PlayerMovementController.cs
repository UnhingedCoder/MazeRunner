using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private float speed = 10f;

    private bool isMoving = false;

    private bool isRightPressed = false;
    private bool isLeftPressed = false;
    private bool isUpPressed = false;
    private bool isDownPressed = false;

    [SerializeField] private bool canRollRight = false;
    [SerializeField] private bool canRollLeft = false;
    [SerializeField] private bool canRollUp = false;
    [SerializeField] private bool canRollDown = false;

    #endregion

    #region UNITY_REG
    private void Update()
    {
        Move();
    }
    #endregion

    #region CLASS_REG
    private void Move()
    {
        if (isMoving)
            return;


        if (isRightPressed && canRollRight)
        {
            StartCoroutine(Roll(Vector3.right));
        }
        else if (isLeftPressed && canRollLeft)
        {
            StartCoroutine(Roll(Vector3.left));
        }
        else if (isUpPressed && canRollUp)
        {
            StartCoroutine(Roll(Vector3.forward));
        }
        else if (isDownPressed && canRollDown)
        {
            StartCoroutine(Roll(Vector3.back));
        }
    }

    IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;

        float remainingAngle = 90f;
        Vector3 rotationCenter = this.transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            this.transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        isMoving = false;

    }

    public void OnMoveInputPressed(MoveInputDirection dir)
    {
        isRightPressed = false;
        isLeftPressed = false;
        isUpPressed = false;
        isDownPressed = false;

        switch (dir)
        {
            case MoveInputDirection.Right:
                {
                    isRightPressed = true;
                }
                break;

            case MoveInputDirection.Left:
                {
                    isLeftPressed = true;
                }
                break;

            case MoveInputDirection.Up:
                {
                    isUpPressed = true;
                }
                break;

            case MoveInputDirection.Down:
                {
                    isDownPressed = true;
                }
                break;

            case MoveInputDirection.None:
            default:
                break;
        }
    }

    public void SetMovementConstraints(bool left = false, bool right = false, bool up = false, bool down = false)
    {
        canRollRight = right;
        canRollLeft = left;
        canRollUp = up;
        canRollDown = down;
    }

    #endregion
}
