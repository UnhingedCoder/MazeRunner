using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SciencePuzzleSelectedChoiceView : MonoBehaviour
{
    #region VARIABLE_REG
    public SciencePuzzle sciencePuzzle;
    [SerializeField] private GameObject m_selectedStatusOverlay;

    public Image icon;
    public TextMeshProUGUI puzzleName;

    private bool isChoiceUsed = false;

    private SciencePuzzleManager m_puzzleManager;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_puzzleManager = FindObjectOfType<SciencePuzzleManager>();
    }

    #endregion

    #region CLASS_REG

    public void OnChoiceClicked()
    {
        if (!m_puzzleManager.isPuzzleActive)
            return;

        if (!isChoiceUsed)
        {
            if (m_puzzleManager.isPuzzleSlotFull)
                return;

            m_puzzleManager.InputOperands(sciencePuzzle);
            isChoiceUsed = true;
        }
        else
        {
            m_puzzleManager.RemoveOperands(sciencePuzzle);
            isChoiceUsed = false;
        }


        m_selectedStatusOverlay.SetActive(isChoiceUsed);
    }

    public void ResetChoiceSelection()
    {
        isChoiceUsed = false;

        m_selectedStatusOverlay.SetActive(isChoiceUsed);
    }
    #endregion
}
