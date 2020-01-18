using System.Collections;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    [Header("The \"correct obj\" puzzle triggers required for this puzzle")]
    [SerializeField] private PuzzleTrigger[] correctObjConditions;

    [Header("The \"target\" puzzle triggers require for this puzzle")]
    [SerializeField] private TargetTrigger[] targetConditions;

    private AudioSource dooraudiosource;
    private Animator doorAnim;

    private int numberOfConditions = 0;
    private int numberOfConditionsMet = 0;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();
        numberOfConditions = correctObjConditions.Length + targetConditions.Length;
        dooraudiosource = GetComponent<AudioSource>();
        foreach (PuzzleTrigger puzzle in correctObjConditions)
        {
            puzzle.OnPuzzleComplete += Puzzle_OnPuzzleComplete;
            puzzle.OnObjRemove += Puzzle_OnObjRemove;
        }
        foreach (TargetTrigger trigger in targetConditions)
        {
            trigger.OnPuzzleComplete += Trigger_OnPuzzleComplete;
        }
    }

    private void Trigger_OnPuzzleComplete()
    {
        numberOfConditionsMet++;
        if (numberOfConditionsMet == numberOfConditions)
        {
            SolvePuzzle();
        }
    }

    private void Puzzle_OnObjRemove()
    {
        numberOfConditionsMet--; // If puzzleObj *was* in correct place but player moves it off
    }

    private void Puzzle_OnPuzzleComplete()
    {
        numberOfConditionsMet++;
        if (numberOfConditionsMet == numberOfConditions)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        doorAnim.SetTrigger("Open");
        dooraudiosource.Play();
    }
}
