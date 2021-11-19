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

    private void Start()
    {
        StartCoroutine(OnFirstLoad());
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

    public void OnScienceClicked()
    {
        m_levelManager.LoadScienceLevel();
    }

    public void OnEnglishClicked()
    {
        m_levelManager.LoadEnglishLevel();
    }
    public void OnBackClicked()
    {
        startMenu.SetActive(true);
        subjectsMenu.SetActive(false);
    }

    IEnumerator OnFirstLoad()
    {
        yield return new WaitForSeconds(0.3f);

        OnBackClicked();
    }
    #endregion
}
