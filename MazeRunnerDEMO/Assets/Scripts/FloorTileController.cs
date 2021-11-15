using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileController : MonoBehaviour
{
    #region VARIABLE_REG
    public bool allowLeftMovement;
    public bool allowRightMovement;
    public bool allowUpMovement;
    public bool allowDownMovement;

    private PlayerMovementController m_playerMovementController;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_playerMovementController = FindObjectOfType<PlayerMovementController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerMovementConstraints();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetPlayerMovementConstraints();
        }
    }
    #endregion

    #region CLASS_REG
    private void SetPlayerMovementConstraints()
    {
        m_playerMovementController.SetMovementConstraints(allowLeftMovement, allowRightMovement, allowUpMovement, allowDownMovement);
    }

    private void ResetPlayerMovementConstraints()
    {
        m_playerMovementController.SetMovementConstraints();
    }
    #endregion
}
