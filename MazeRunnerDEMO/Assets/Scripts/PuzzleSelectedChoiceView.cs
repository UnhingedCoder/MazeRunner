using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleSelectedChoiceView : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private TextMeshProUGUI m_choiceText;
    [SerializeField] private GameObject m_selectedStatusOverlay;

    private bool isChoiceUsed = false;

    private PuzzleManager m_puzzleManager;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    private void OnEnable()
    {
        PopulateSelectedChoiceSlot();
    }
    #endregion

    #region CLASS_REG
    public void PopulateSelectedChoiceSlot(string choiceVal = "")
    {
        m_choiceText.text = choiceVal;

        m_selectedStatusOverlay.SetActive(isChoiceUsed);
    }

    public void OnChoiceClicked()
    {
        if (!m_puzzleManager.isPuzzleActive)
            return;

        if (!isChoiceUsed)
        {
            m_puzzleManager.InputOperands(m_choiceText.text);
            isChoiceUsed = true;
        }
        else
        {
            m_puzzleManager.RemoveOperands(m_choiceText.text);
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
