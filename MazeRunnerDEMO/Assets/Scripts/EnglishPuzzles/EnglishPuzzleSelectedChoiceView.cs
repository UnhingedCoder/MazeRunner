using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnglishPuzzleSelectedChoiceView : MonoBehaviour
{
    #region VARIABLE_REG
    public EnglishPuzzle englishPuzzle;
    [SerializeField] private GameObject m_selectedStatusOverlay;

    public TextMeshProUGUI puzzleName;

    private bool isChoiceUsed = false;

    private EnglishPuzzleManager m_puzzleManager;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_puzzleManager = FindObjectOfType<EnglishPuzzleManager>();
    }

    private void OnEnable()
    {
        puzzleName.text = englishPuzzle.PuzzleName;
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

            m_puzzleManager.InputOperands(englishPuzzle);
            isChoiceUsed = true;
        }
        else
        {
            m_puzzleManager.RemoveOperands(englishPuzzle);
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
