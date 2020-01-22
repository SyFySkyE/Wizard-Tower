using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNarrative : MonoBehaviour
{
    [SerializeField] private AudioClip firstEnter;
    [SerializeField] private AudioClip lookByBookshelf;
    [SerializeField] private AudioClip firstDoor;
    [SerializeField] private AudioClip secondDoor;
    [SerializeField] private AudioClip passByDaughtersRoom;
    [SerializeField] private AudioClip walkInDaughters;
    [SerializeField] private AudioClip stareAtBed;
    [SerializeField] private AudioClip stairwellPictures;
    [SerializeField] private AudioClip stairwellPictures2;
    [SerializeField] private AudioClip torchHint;
    [SerializeField] private AudioClip seeVespera;
    [SerializeField] private AudioClip vesperaSpeak;

    [SerializeField] private float robVolume = 3f;

    private AudioSource playerAudio;
    private NarrativeTrigger[] narrativeTriggers;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        narrativeTriggers = FindObjectsOfType<NarrativeTrigger>();

        foreach (NarrativeTrigger narrativeTrigger in narrativeTriggers)
        {
            narrativeTrigger.OnNarrativeEvent += NarrativeTrigger_OnNarrativeEvent;
        }
    }

    private void NarrativeTrigger_OnNarrativeEvent(NarrativeTrigger.NarrativeState obj)
    {
        playerAudio.Stop();
        switch (obj)
        {
            case NarrativeTrigger.NarrativeState.FirstEnter:
                playerAudio.PlayOneShot(firstEnter, robVolume);
                break;
            case NarrativeTrigger.NarrativeState.LookAtBookShelf:
                playerAudio.PlayOneShot(lookByBookshelf, robVolume);
                break;
            case NarrativeTrigger.NarrativeState.FirstDoor:
                playerAudio.PlayOneShot(firstDoor, robVolume);
                break;
            case NarrativeTrigger.NarrativeState.SecondDoor:
                playerAudio.PlayOneShot(secondDoor, robVolume);
                break;
            case NarrativeTrigger.NarrativeState.PassDaughters:
                playerAudio.PlayOneShot(passByDaughtersRoom, robVolume);
                break;
            case NarrativeTrigger.NarrativeState.DaughtersRoom:
                playerAudio.PlayOneShot(walkInDaughters, robVolume);
                break;
            case NarrativeTrigger.NarrativeState.Bed:
                playerAudio.PlayOneShot(stareAtBed, robVolume);
                break;
            case NarrativeTrigger.NarrativeState.StairwayPictures:
                playerAudio.PlayOneShot(stairwellPictures, robVolume);
                break;
            case NarrativeTrigger.NarrativeState.StairwayPicturesTwo:                
                playerAudio.PlayOneShot(stairwellPictures2, robVolume);
                break;
            case NarrativeTrigger.NarrativeState.TorchHint:
                playerAudio.PlayOneShot(torchHint, 1);
                break;
            case NarrativeTrigger.NarrativeState.SeeVespera:
                playerAudio.PlayOneShot(seeVespera, robVolume);
                break;
            case NarrativeTrigger.NarrativeState.VesperaSpeak:
                playerAudio.PlayOneShot(vesperaSpeak, robVolume);
                break;
        }
    }
}
