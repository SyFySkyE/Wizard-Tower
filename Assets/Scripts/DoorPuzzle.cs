using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    [Header("The puzzle triggers needed to complete this puzzle")]
    [SerializeField] private PuzzleTrigger[] puzzleConditions;

    private Animator doorAnimator;

    private int numberOfConditionsMet = 0;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        foreach( PuzzleTrigger puzzle in puzzleConditions)
        {
            puzzle.OnPuzzleComplete += Puzzle_OnPuzzleComplete;
        }
    }

    private void Puzzle_OnPuzzleComplete()
    {
        numberOfConditionsMet++;
        if (numberOfConditionsMet == puzzleConditions.Length)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        doorAnimator.SetTrigger("Open");
    }
}
