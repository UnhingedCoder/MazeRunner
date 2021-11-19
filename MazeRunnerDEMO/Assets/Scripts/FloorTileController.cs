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

    public bool isPuzzleSolved;
    public bool isPuzzleAlotted = false;
    public bool isExit = false;

    public int operand1;
    public int operand2;
    public MathOperator mathOperator1;
    public int result;
    public SciencePuzzle sciencePuzzle;
    public EnglishPuzzle englishPuzzle;

    public int waitTime;
    public bool isPuzzleTriggered = false;

    [SerializeField] private GameObject m_tileStepFX;
    [SerializeField] private GameObject m_unlockFX;
    [SerializeField] private GameObject m_tileBase;
    [SerializeField] private Material m_solvedMat;
    [SerializeField] private Material m_unsolvedMat;

    [SerializeField] private FloorTileManager m_floorTileManager;
    private PlayerMovementController m_playerMovementController;
    private PuzzleManager m_puzzleManager;
    private SciencePuzzleManager m_SciencePuzzleManager;
    private EnglishPuzzleManager m_EnglishPuzzleManager;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_playerMovementController = FindObjectOfType<PlayerMovementController>();
        m_puzzleManager = FindObjectOfType<PuzzleManager>();
        m_SciencePuzzleManager = FindObjectOfType<SciencePuzzleManager>();
        m_EnglishPuzzleManager = FindObjectOfType<EnglishPuzzleManager>();
    }

    private void OnValidate()
    {
        CheckIfPuzzleIsSolved();
    }

    private void OnEnable()
    {
        ResetTile();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerMovementConstraints();
            m_floorTileManager.CheckForPuzzleTile();

            if (isExit)
            {
                if (m_puzzleManager != null)
                    m_puzzleManager.exitMazeEvent.Invoke();

                if (m_SciencePuzzleManager != null)
                    m_SciencePuzzleManager.exitMazeEvent.Invoke();

                if (m_EnglishPuzzleManager != null)
                    m_EnglishPuzzleManager.exitMazeEvent.Invoke();
            }

        }
    }

    private void OnTriggerStay(Collider other)
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
            m_tileStepFX.SetActive(true);
            m_floorTileManager.SkipPuzzleTile();
        }
    }
    #endregion

    #region CLASS_REG
    private void ResetTile()
    {
        allowLeftMovement = false;
        allowRightMovement = false;
        allowUpMovement = false;
        allowDownMovement = false;
    }

    private void SetPlayerMovementConstraints()
    {
        m_playerMovementController.SetMovementConstraints(allowLeftMovement, allowRightMovement, allowUpMovement, allowDownMovement);
    }

    private void ResetPlayerMovementConstraints()
    {
        m_playerMovementController.SetMovementConstraints();
    }

    [ContextMenu("SolvePuzzle")]
    public bool CheckIfPuzzleIsSolved()
    {
        if (!isExit)
        {
            if (isPuzzleSolved)
                m_tileBase.GetComponent<Renderer>().material = m_solvedMat;
            else
                m_tileBase.GetComponent<Renderer>().material = m_unsolvedMat;
        }

        return isPuzzleSolved;
    }

    public void SolvePuzzle()
    {
        isPuzzleSolved = true;
        m_unlockFX.SetActive(true);
        CheckIfPuzzleIsSolved();
    }
    #endregion
}
