﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private AudioSource playerAudioSource;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayWalkSfx();
    }

    private void PlayWalkSfx()
    {
        if (playerRb.velocity != Vector3.zero)
        {
            if (!playerAudioSource.isPlaying)
            {
                playerAudioSource.Play();
            }
        }
        else
        {
            playerAudioSource.Stop();
        }
    }
}
