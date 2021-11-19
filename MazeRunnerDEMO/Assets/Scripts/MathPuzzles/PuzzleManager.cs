using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public enum MathOperator
{
    Add,
    Subtract,
    Muliply,
    Divide
}

public class PuzzleManager : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private TextMeshProUGUI operand_1Text;
    [SerializeField] private TextMeshProUGUI operand_2Text;
    [SerializeField] private TextMeshProUGUI operator_1Text;
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private int[] m_numberRange;

    [SerializeField] private PuzzleChoiceView[] m_puzzleChoices;
    [SerializeField] private List<PuzzleSelectedChoiceView> m_selectedPuzzleChoices;

    [SerializeField] private List<int> m_selectedChoices;

    [SerializeField] private int m_maxSelectedChoices;

    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject m_moveButtons;
    [SerializeField] private GameObject m_puzzleDialog;
    [SerializeField] private GameObject m_ChoicesButtons;
    [SerializeField] private GameObject m_startButton;
    [SerializeField] private GameObject m_goButton;

    [SerializeField] private List<int> listOfSums = new List<int>();

    public bool isChoiceSlotsFilled = false;

    public bool isPuzzleActive = false;
    public bool isPuzzleSlotFull = false;


    [SerializeField] private int operand1 = -99;
    [SerializeField] private int operand2 = -99;
    [SerializeField] private MathOperator mathOperator1;
    [SerializeField] private int result = -99;

    private FloorTileController m_puzzleTileRef;
    private LevelManager m_levelManager;

    public UnityEvent<FloorTileController> triggerPuzzleEvent;
    public UnityEvent hidePuzzleEvent;
    public UnityEvent exitMazeEvent;

    public List<int> SelectedChoices { get => m_selectedChoices; }
    public int MaxSelectedChoices { get => m_maxSelectedChoices; }
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        triggerPuzzleEvent.AddListener(PopulatePuzzle);
        hidePuzzleEvent.AddListener(HidePuzzle);
        exitMazeEvent.AddListener(OnExitReached);

        m_levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnEnable()
    {
        SetChoices();
        m_startButton.SetActive(false);
        DisplayPuzzle();
    }
    #endregion

    #region CLASS_REG
    public void SetChoices()
    {
        for (int i = 0; i < m_puzzleChoices.Length; i++)
        {
            m_puzzleChoices[i].SetChoiceText(m_numberRange[i]);
        }
    }
    public void AddChoice(int choiceVal)
    {
        AddPickupToSelectedChoices(choiceVal);

        if (m_selectedChoices.Count == m_maxSelectedChoices)
        {
            isChoiceSlotsFilled = true;
            m_startButton.SetActive(true);
        }
        else
        {
            m_startButton.SetActive(false);

        }

    }

    public void AddPickupToSelectedChoices(int choiceVal)
    {
        if (m_selectedChoices.Count < m_maxSelectedChoices)
        {
            m_selectedChoices.Add(choiceVal);
        }

        PopulateSelectedSlots();
    }

    public void RemoveChoice(int choiceVal)
    {
        if (m_selectedChoices.Contains(choiceVal))
        {
            m_selectedPuzzleChoices[m_selectedChoices.IndexOf(choiceVal)].PopulateSelectedChoiceSlot("");
            m_selectedChoices.Remove(choiceVal);
            isChoiceSlotsFilled = false;
            m_startButton.SetActive(false);
        }

        PopulateSelectedSlots();
    }

    private void PopulateSelectedSlots()
    {
        for (int i = 0; i < m_selectedPuzzleChoices.Count; i++)
        {
            if (i < m_selectedChoices.Count)
            {
                string val = m_selectedChoices[i].ToString();
                m_selectedPuzzleChoices[i].PopulateSelectedChoiceSlot(val);
            }
            else
            {
                m_selectedPuzzleChoices[i].PopulateSelectedChoiceSlot();
            }
        }
    }

    public void OnStartClicked()
    {
        m_ChoicesButtons.SetActive(false);
        m_startButton.SetActive(false);
        m_moveButtons.SetActive(true);
        m_player.transform.DOMoveY(0.5f, 1.3f);
    }

    private int CheckSumPairsPuzzle(List<int> lst, int sum)
    {
        // Consider all possible pairs
        // and check their sums
        for (int i = 0; i < lst.Count; i++)
        {
            for (int j = i + 1; j < lst.Count; j++)
            {
                if ((lst[i] + lst[j]) == sum)
                {
                    Debug.Log(lst[i] + "+" + lst[j] + "=" + sum);
                    return sum;
                }
            }
        }

        return -99;
    }

    private void PopulatePuzzle(FloorTileController puzzleTile)
    {
        m_puzzleTileRef = puzzleTile;

        isPuzzleActive = true;
        m_puzzleDialog.SetActive(true);

        if (puzzleTile.isPuzzleAlotted)
        {
            resultText.text = puzzleTile.result.ToString();
            result = puzzleTile.result;
            return;
        }

        listOfSums.Clear();
        for (int i = -10; i < 11; i++)
        {
            int c = CheckSumPairsPuzzle(m_selectedChoices, i);
            if (c > -90 && !listOfSums.Contains(c))
                listOfSums.Add(c);
        }

        int index = Random.Range(0, listOfSums.Count);

        if (index < listOfSums.Count)
        {
            puzzleTile.mathOperator1 = MathOperator.Add;
            puzzleTile.result = listOfSums[index];

            resultText.text = puzzleTile.result.ToString();
            result = puzzleTile.result;

            puzzleTile.isPuzzleAlotted = true;
        }

    }

    private void HidePuzzle()
    {
        m_puzzleTileRef = null;
        m_puzzleDialog.SetActive(false);
        isPuzzleActive = false;
        isPuzzleSlotFull = false;
        operand1 = -99;
        operand2 = -99;
        result = -99;
        DisplayPuzzle();

        for (int i = 0; i < m_selectedPuzzleChoices.Count; i++)
        {
            m_selectedPuzzleChoices[i].ResetChoiceSelection();
        }
    }


    public void InputOperands(string choiceStr)
    {
        if (!isPuzzleActive)
            return;

        int choiceVal = int.Parse(choiceStr);

        if (operand1 == -99)
            operand1 = choiceVal;
        else if(operand2 == -99)
            operand2 = choiceVal;

        if (operand1 > -99 && operand2 > -99)
            isPuzzleSlotFull = true;

        DisplayPuzzle();
    }

    public void RemoveOperands(string choiceStr)
    {
        if (!isPuzzleActive)
            return;

        int choiceVal = int.Parse(choiceStr);
        if (operand1 == choiceVal)
            operand1 = -99;
        else if (operand2 == choiceVal)
            operand2 = -99;

        isPuzzleSlotFull = false;

        DisplayPuzzle();
    }

    private void DisplayPuzzle()
    {
        operand_1Text.text = operand1.ToString();
        operand_2Text.text = operand2.ToString();


        if (operand1 == -99)
            operand_1Text.text = "_";

        if(operand2 == -99)
            operand_2Text.text = "_";

        if (operand1 != -99 && operand2 != -99)
            m_goButton.SetActive(true);
        else
            m_goButton.SetActive(false);

    }

    public void OnGoClicked()
    {
        Debug.Log("GO CLICKED");

        if (operand1 + operand2 == result)
        {
            m_puzzleTileRef.SolvePuzzle();

            isChoiceSlotsFilled = false;

            PopulateSelectedSlots();
            HidePuzzle();
        }
        else
            m_goButton.GetComponent<Animator>().SetTrigger("Wrong");

    }


    private void OnExitReached()
    {
        StartCoroutine(ExitCoroutine());
    }
    IEnumerator ExitCoroutine()
    {
        yield return new WaitForSeconds(0.3f);

        Tween _tween = m_player.transform.DOMoveY(35f, 3f);
        yield return _tween.WaitForCompletion();

        m_levelManager.LoadSubjectsLevel();

        Debug.Log("Tween completed!");
    }

    public void OnBackClicked()
    {
        m_levelManager.LoadMainMenu();
    }
}
    #endregion

