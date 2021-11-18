using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private List<int> pickupRange;

    private PuzzleManager m_puzzleManager;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_puzzleManager.SelectedChoices.Count >= m_puzzleManager.MaxSelectedChoices)
                return;

            List<int> lst = m_puzzleManager.SelectedChoices;
            List<int> newPickupRange = new List<int>();
            for (int i = 0; i < pickupRange.Count; i++)
            {
                if (!lst.Contains(i))
                {
                    newPickupRange.Add(i);
                }
            }

            int index = Random.Range(0, newPickupRange.Count);

            if (index < newPickupRange.Count)
            {
                m_puzzleManager.AddPickupToSelectedChoices(newPickupRange[index]);
            }
            this.gameObject.SetActive(false);
        }
    }
    #endregion

    #region CLASS_REG
    #endregion
}
