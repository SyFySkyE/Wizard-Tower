using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPuzzle : MonoBehaviour
{
    [Header("The target triggers needed to complete this puzzle")]
    [SerializeField] private TargetTrigger[] puzzleConditions;

    private Animator doorAnim;
    private AudioSource doorAudioSource;
    private int numberOfConditionsMet = 0;

    // Start is called before the first frame update
    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
        doorAnim = GetComponent<Animator>();
        foreach (TargetTrigger targetTrigger in puzzleConditions)
        {
            targetTrigger.OnPuzzleComplete += TargetTrigger_OnPuzzleComplete;
        }
    }

    private void TargetTrigger_OnPuzzleComplete()
    {
        numberOfConditionsMet++;
        if (numberOfConditionsMet == puzzleConditions.Length)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        doorAnim.SetTrigger("Open");
        doorAudioSource.Play();
    }
}
