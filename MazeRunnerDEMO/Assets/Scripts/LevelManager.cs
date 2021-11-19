using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region VARIABLE_REG
    #endregion

    #region UNITY_REG
    #endregion

    #region CLASS_REG

    public void LoadMathLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScienceLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadEnglishLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadSubjectsLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex < 3)
        {
            SceneManager.LoadScene(scene.buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
