using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    #region VARIABLE_REG
    public GameObject startMenu;
    public GameObject subjectsMenu;

    private LevelManager m_levelManager;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnEnable()
    {
        OnBackClicked();
    }
    #endregion

    #region CLASS_REG
    public void OnPlayClicked()
    {
        startMenu.SetActive(false);
        subjectsMenu.SetActive(true);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnMathsClicked()
    {
        m_levelManager.LoadMathLevel();
    }

    public void OnBackClicked()
    {
        startMenu.SetActive(true);
        subjectsMenu.SetActive(false);
    }
    #endregion
}
