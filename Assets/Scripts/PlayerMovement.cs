using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private AudioSource playerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !playerAudioSource.isPlaying)
        {
            playerAudioSource.Play();
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            playerAudioSource.Stop();
        }
    }
}
