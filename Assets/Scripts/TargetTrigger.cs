using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    [Header("SFX that plays when put in right place")]
    [SerializeField] private AudioClip puzzleCompleteSfx;
    [SerializeField] private float puzzleCompleteSfxVolume = 0.5f;

    private AudioSource puzzleAudioSource;
    private ParticleSystem puzzleCompleteVfx;
    private bool hasCompleted = false;

    public event System.Action OnPuzzleComplete;

    // Start is called before the first frame update
    void Start()
    {
        puzzleCompleteVfx = GetComponentInChildren<ParticleSystem>();
        puzzleAudioSource = GetComponent<AudioSource>();
        this.tag = "Target";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !hasCompleted)
        {
            OnPuzzleComplete();
            puzzleCompleteVfx.Play();
            puzzleAudioSource.PlayOneShot(puzzleCompleteSfx, puzzleCompleteSfxVolume);
            hasCompleted = true;
        }
    }
}
