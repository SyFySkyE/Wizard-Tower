using System.Collections;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour // TODO Tech debt is pretty high in this script
{
    [Header("The \"correct obj\" puzzle triggers required for this puzzle")]
    [SerializeField] private PuzzleTrigger[] correctObjConditions;

    [Header("The \"target\" puzzle triggers require for this puzzle")]
    [SerializeField] private TargetTrigger[] targetConditions;

    private AudioSource dooraudiosource;
    private Animator doorAnim;
    private UnityEngine.UI.Slider progressSlider;

    private int numberOfConditions = 0;
    private int numberOfConditionsMet = 0;
    private bool isSolved = false;

    // Start is called before the first frame update
    void Start()
    {        
        doorAnim = GetComponent<Animator>();        
        dooraudiosource = GetComponent<AudioSource>();
        progressSlider = GetComponentInChildren<UnityEngine.UI.Slider>();
        SubscribeToPuzzleConditions();        
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

    private void Trigger_OnPuzzleComplete()
    {
        numberOfConditionsMet++;
        progressSlider.value++;
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
    }
}
