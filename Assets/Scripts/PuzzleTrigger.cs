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
        if (!correctObjTrigger)
        {
            Debug.LogError(name + " does NOT have a \"Correct Obj Trigger object\" in its slot. Remember to drag the object needed to be place here, and make it sure it has the \"Pickup\" script component attached as this puzzle CANNOT be completed without an object in this slot!!");
        }
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

    public void ResetPuzzle()
    {
        correctObjTrigger.GetComponent<PickupObject>().ResetPostion();
    }
}
