using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    public event System.Action<NarrativeState> OnNarrativeEvent;

    public enum NarrativeState { FirstEnter, LookAtBookShelf, FirstDoor, SecondDoor, PassDaughters, DaughtersRoom, Bed, StairwayPictures, StairwayPicturesTwo }

    [SerializeField] private NarrativeState currentState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnNarrativeEvent(currentState);
            Destroy(this.gameObject);
        }
    }
}
