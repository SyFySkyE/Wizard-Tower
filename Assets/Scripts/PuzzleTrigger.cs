using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    [Header("What correct object goes here")]
    [SerializeField] private GameObject correctObjTrigger;

    [Header("SFX that plays when put in right place")]
    [SerializeField] private AudioClip puzzleCompleteSfx;
    [SerializeField] private float puzzleCompleteSfxVolume = 0.5f;

    private AudioSource puzzleAudioSource;
    private ParticleSystem puzzleCompleteVfx;

    public event System.Action OnPuzzleComplete;
    public event System.Action OnObjRemove; // If player removes obj off of correct place

    private void Start()
    {
        puzzleCompleteVfx = GetComponentInChildren<ParticleSystem>();
        puzzleAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == correctObjTrigger)
        {
            puzzleAudioSource.PlayOneShot(puzzleCompleteSfx, puzzleCompleteSfxVolume);
            puzzleCompleteVfx.Play();
            OnPuzzleComplete();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == correctObjTrigger)
        {
            OnObjRemove();
        }
    }
}
