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

    public void LoadSubjectsLevel()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
