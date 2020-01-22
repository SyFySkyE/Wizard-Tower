using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : MonoBehaviour
{
    [SerializeField] private GameObject fireEffects;

    [SerializeField] private AudioClip fireLight;
    [SerializeField] private float fireLightVolume = 0.5f;
   
    private AudioSource fireplaceAudioSource;

    private void Start()
    {
        fireplaceAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            fireEffects.SetActive(true);
            AudioSource.PlayClipAtPoint(fireLight, transform.position, fireLightVolume);
            fireplaceAudioSource.Play();
        }
    }
}
