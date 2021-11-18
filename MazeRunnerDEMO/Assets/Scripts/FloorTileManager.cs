using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileManager : MonoBehaviour
{
    #region VARIABLE_REG
    public FloorTileController floorTileController;
    private PuzzleManager m_puzzleManager;
    private SciencePuzzleManager m_SciencePuzzleManager;

    [SerializeField]private FloorTileController tile = null;


    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_puzzleManager = FindObjectOfType<PuzzleManager>();
        m_SciencePuzzleManager = FindObjectOfType<SciencePuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TileBase"))
        {
            SetMovementParameters(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TileBase"))
        {
            SetMovementParameters(other);
        }
    }
    #endregion

    #region CLASS_REG
    private void SetMovementParameters(Collider other)
    {
        FloorTileController adjacentTile = other.GetComponent<FloorTileManager>().floorTileController;

        //Left & Right
        if (other.transform.position.x > this.transform.position.x && other.transform.position.z == this.transform.position.z && adjacentTile.isPuzzleSolved)
        {
            floorTileController.allowRightMovement = true;
        }
        if (other.transform.position.x < this.transform.position.x && other.transform.position.z == this.transform.position.z && adjacentTile.isPuzzleSolved)
        {
            floorTileController.allowLeftMovement = true;
        }

        //Up & Down
        if (other.transform.position.z > this.transform.position.z && other.transform.position.x == this.transform.position.x && adjacentTile.isPuzzleSolved)
        {
            floorTileController.allowUpMovement = true;
        }
        if (other.transform.position.z < this.transform.position.z && other.transform.position.x == this.transform.position.x && adjacentTile.isPuzzleSolved)
        {
            floorTileController.allowDownMovement = true;
        }

        if (!adjacentTile.isPuzzleSolved)
            tile = adjacentTile;
    }

    public void CheckForPuzzleTile()
    {
        Debug.Log("CheckForPuzzleTile");
        if (tile == null)
            return;
        
        if (!tile.isPuzzleSolved)
        {
            Debug.Log("TRIGGER PUZZLE ON" + tile.transform.parent.name +" FROM "+ this.transform.name);
            if(m_puzzleManager != null)
                m_puzzleManager.triggerPuzzleEvent.Invoke(tile);

            if (m_SciencePuzzleManager != null)
                m_SciencePuzzleManager.triggerPuzzleEvent.Invoke(tile);
        }
    }

    public void SkipPuzzleTile()
    {
        Debug.Log("SkipPuzzleTile");
        if (tile == null)
            return;
        
        if (!tile.isPuzzleSolved)
        {
            Debug.Log("HIDE PUZZLE ON" + tile.transform.parent.name + " FROM " + this.transform.name);
            if (m_puzzleManager != null)
                m_puzzleManager.hidePuzzleEvent.Invoke();


            if (m_SciencePuzzleManager != null)
                m_SciencePuzzleManager.hidePuzzleEvent.Invoke();
        }
        
    }
    #endregion
}
