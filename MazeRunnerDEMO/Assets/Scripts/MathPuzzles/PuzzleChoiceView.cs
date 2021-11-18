using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleChoiceView : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private TextMeshProUGUI m_choiceText;
    [SerializeField] private GameObject m_statusOverlay;

    private int m_choiceVal;
    private bool m_isChoiceSelected = false;

    private PuzzleManager m_puzzleManager;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    #endregion

    #region CLASS_REG
    public void SetChoiceText(int choice)
    {

        m_choiceText.text = choice.ToString();
        m_choiceVal = choice;
    }

    public void OnChoiceClicked()
    {
        if (m_isChoiceSelected)
        {
            m_statusOverlay.SetActive(false);
            m_puzzleManager.RemoveChoice(m_choiceVal);
            m_isChoiceSelected = false;
        }
        else
        {
            if (!m_puzzleManager.isChoiceSlotsFilled)
            {
                m_statusOverlay.SetActive(true);
                m_puzzleManager.AddChoice(m_choiceVal);
                m_isChoiceSelected = true;
            }
        }
    }
    #endregion
}
