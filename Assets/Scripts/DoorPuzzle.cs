using System.Collections;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour // TODO Tech debt is pretty high in this script
{
    [Header("The \"correct obj\" puzzle triggers required for this puzzle")]
    [SerializeField] private PuzzleTrigger[] correctObjConditions;

    [Header("The \"target\" puzzle triggers require for this puzzle")]
    [SerializeField] private TargetTrigger[] targetConditions;

    [Header("How many sec's before door closes. Leave this at 0 if not a Time puzzle.")]
    [Tooltip("How many seconds between the puzzle being solved and the door opening, and the door closing. This is meant for Time puzzles, where when the puzzle is solved, the player has a limited time before the door closes and the puzzle reset. Leave this at 0 if you do not want this to be a time puzzle.")]
    [SerializeField] private float secondsBeforePuzzleReset = 0f;

    private AudioSource dooraudiosource;
    private Animator doorAnim;
    private UnityEngine.UI.Slider progressSlider;
    private TMPro.TextMeshProUGUI progressText;

    private int numberOfConditions = 0;
    private int numberOfConditionsMet = 0;
    private bool isSolved = false;
    private bool isTimePuzzleSolved = false;

    public event System.Action OnPuzzleReset;

    // Start is called before the first frame update
    void Start()
    {
        progressText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        doorAnim = GetComponent<Animator>();        
        dooraudiosource = GetComponent<AudioSource>();
        progressSlider = GetComponentInChildren<UnityEngine.UI.Slider>();
        SubscribeToPuzzleConditions();
        CaculateStartProgressText();
        if (secondsBeforePuzzleReset == 0)
        {
            progressSlider.gameObject.SetActive(false);
        }        
    }

    private void SubscribeToPuzzleConditions()
    {
        numberOfConditions = correctObjConditions.Length + targetConditions.Length;
        progressSlider.maxValue = numberOfConditions;

        foreach (TargetTrigger trigger in targetConditions)
        {
            trigger.OnPuzzleComplete += Trigger_OnPuzzleComplete;
        }

        foreach (PuzzleTrigger puzzle in correctObjConditions)
        {
            puzzle.OnPuzzleComplete += Puzzle_OnPuzzleComplete;
            puzzle.OnObjRemove += Puzzle_OnObjRemove;
        }
    }

    private void CaculateStartProgressText()
    {
        progressText.text = "";
        for (int i = 0; i < numberOfConditions; i++)
        {
            progressText.text += " X ";
        }
    }

    private void Trigger_OnPuzzleComplete()
    {
        numberOfConditionsMet++;
        progressSlider.value++;
        progressText.text = progressText.text.Remove(0, 3);
        if (numberOfConditionsMet == numberOfConditions)
        {
            SolvePuzzle();
        }
    }

    private void Puzzle_OnObjRemove()
    {
        if (!isSolved)
        {
            numberOfConditionsMet--; // If puzzleObj *was* in correct place but player moves it off
            progressSlider.value--;
        }        
    }

    private void Puzzle_OnPuzzleComplete()
    {
        if (!isSolved)
        {
            numberOfConditionsMet++;
            progressSlider.value++;
            if (numberOfConditionsMet == numberOfConditions)
            {
                SolvePuzzle();
            }
        }        
    }

    private void SolvePuzzle()
    {
        doorAnim.SetTrigger("Open");
        isSolved = true;
        dooraudiosource.Play();
        if (secondsBeforePuzzleReset != 0)
        {
            StartCoroutine(CloseDoor());
        }
    }

    private IEnumerator CloseDoor()
    {
        progressSlider.maxValue = secondsBeforePuzzleReset;
        for (float i = secondsBeforePuzzleReset; i > 0; i--)
        {
            progressSlider.value = i;
            yield return new WaitForSeconds(1);
        }                
        ResetPuzzle();        
    }

    private void ResetPuzzle()
    {
        if (!isTimePuzzleSolved)
        {
            foreach (TargetTrigger trigger in targetConditions)
            {
                trigger.ResetPuzzle();
            }

            foreach (PuzzleTrigger puzzle in correctObjConditions)
            {
                puzzle.ResetPuzzle();
            }
            numberOfConditionsMet = correctObjConditions.Length;
            progressSlider.maxValue = numberOfConditions;
            progressSlider.value = numberOfConditionsMet;
            CaculateStartProgressText();
            doorAnim.SetTrigger("Close");
            isSolved = false;
        }        
    }
}
