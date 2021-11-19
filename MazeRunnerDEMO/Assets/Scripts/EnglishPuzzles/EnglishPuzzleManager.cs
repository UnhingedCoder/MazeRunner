using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;


public class EnglishPuzzleManager : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private TextMeshProUGUI operand_1Text;
    
    [SerializeField] private List<EnglishPuzzleSelectedChoiceView> m_selectedPuzzleChoices;

    [SerializeField] private List<int> m_selectedChoices;

    [SerializeField] private int m_maxSelectedChoices;

    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject m_moveButtons;
    [SerializeField] private GameObject m_puzzleDialog;
    [SerializeField] private GameObject m_startButton;
    [SerializeField] private GameObject m_goButton;

    [SerializeField] private List<EnglishPuzzle> listOfEnglishPuzzles = new List<EnglishPuzzle>();

    public bool isChoiceSlotsFilled = false;

    public bool isPuzzleActive = false;
    public bool isPuzzleSlotFull = false;


    [SerializeField] private EnglishPuzzle selectedPuzzle = null;
    [SerializeField] private EnglishPuzzle activePuzzle = null;

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
        m_startButton.SetActive(true);
        SetPuzzles();
        DisplayPuzzle();
    }
    #endregion

    #region CLASS_REG

    
    public void OnStartClicked()
    {
        m_startButton.SetActive(false);
        m_moveButtons.SetActive(true);
        m_player.transform.DOMoveY(0.5f, 1.3f);
    }

    private EnglishPuzzle GetPuzzle()
    {
        int index = Random.Range(0, listOfEnglishPuzzles.Count);

        if (index < listOfEnglishPuzzles.Count)
        {
            EnglishPuzzle puzzle = listOfEnglishPuzzles[index];
            listOfEnglishPuzzles.Remove(puzzle);
            return puzzle;
        }

        return null;
    }


    private void PopulatePuzzle(FloorTileController puzzleTile)
    {
        m_puzzleTileRef = puzzleTile;

        isPuzzleActive = true;
        m_puzzleDialog.SetActive(true);

        if (puzzleTile.isPuzzleAlotted)
        {
            activePuzzle = puzzleTile.englishPuzzle;
            DisplayPuzzle();
            return;
        }



        EnglishPuzzle puzzle = GetPuzzle();

        activePuzzle = puzzle;
        DisplayPuzzle();

    }

    private void SetPuzzles()
    {
        listOfEnglishPuzzles.Clear();

        for (int i = 0; i < m_selectedPuzzleChoices.Count; i++)
        {
            listOfEnglishPuzzles.Add(m_selectedPuzzleChoices[i].englishPuzzle);
        }
    }

    private void HidePuzzle()
    {
        m_puzzleTileRef = null;
        m_puzzleDialog.SetActive(false);
        isPuzzleActive = false;
        isPuzzleSlotFull = false;
        selectedPuzzle = null;
        activePuzzle = null;
        DisplayPuzzle();

        for (int i = 0; i < m_selectedPuzzleChoices.Count; i++)
        {
            m_selectedPuzzleChoices[i].ResetChoiceSelection();
        }
    }


    public void InputOperands(EnglishPuzzle choice)
    {
        if (!isPuzzleActive)
            return;

        selectedPuzzle = choice;


        if (selectedPuzzle != null)
            isPuzzleSlotFull = true;

        DisplayPuzzle();
    }

    public void RemoveOperands(EnglishPuzzle choice)
    {
        if (!isPuzzleActive)
            return;

        selectedPuzzle = null;

        if (selectedPuzzle == null)
            isPuzzleSlotFull = false;

        DisplayPuzzle();
    }

    private void DisplayPuzzle()
    {
        if(activePuzzle != null)
            operand_1Text.text = activePuzzle.Description;
        else
            operand_1Text.text = "";

        if (selectedPuzzle != null)
            m_goButton.SetActive(true);
        else
            m_goButton.SetActive(false);

    }

    public void OnGoClicked()
    {
        Debug.Log("GO CLICKED");

        if (selectedPuzzle == activePuzzle)
        {
            m_puzzleTileRef.SolvePuzzle();

            isChoiceSlotsFilled = false;
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

