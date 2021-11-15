using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveInputDirection
{
    Right,
    Left,
    Up,
    Down,
    None
}

public class MovementInputHandler : MonoBehaviour
{
    #region VARIABLE_REG
    private PlayerMovementController m_playerMovementController;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_playerMovementController = FindObjectOfType<PlayerMovementController>();
    }
    #endregion

    #region CLASS_REG
    public void MoveRight()
    {
        m_playerMovementController.OnMoveInputPressed(MoveInputDirection.Right);
    }

    public void MoveLeft()
    {
        m_playerMovementController.OnMoveInputPressed(MoveInputDirection.Left);
    }

    public void MoveUp()
    {
        m_playerMovementController.OnMoveInputPressed(MoveInputDirection.Up);
    }

    public void MoveDown()
    {
        m_playerMovementController.OnMoveInputPressed(MoveInputDirection.Down);
    }

    public void StopMovement()
    {
        m_playerMovementController.OnMoveInputPressed(MoveInputDirection.None);
    }
    #endregion
}
