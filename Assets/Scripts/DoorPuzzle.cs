using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    [Header("The puzzle triggers needed to complete this puzzle")]
    [SerializeField] private PuzzleTrigger[] puzzleConditions;

    private Animator doorAnimator;
    private AudioSource dooraudiosource;
    private int numberOfConditionsMet = 0;

    // Start is called before the first frame update
    void Start()
    {
        dooraudiosource = GetComponent<AudioSource>();
        doorAnimator = GetComponent<Animator>();
        foreach (PuzzleTrigger puzzle in puzzleConditions)
        {
            puzzle.OnPuzzleComplete += Puzzle_OnPuzzleComplete;
            puzzle.OnObjRemove += Puzzle_OnObjRemove;
        }
    }

    private void Puzzle_OnObjRemove()
    {
        numberOfConditionsMet--; // If puzzleObj *was* in correct place but player moves it off
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
        dooraudiosource.Play();
    }
}
