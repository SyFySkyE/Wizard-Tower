using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativePotion : MonoBehaviour
{
    private ParticleSystem breakParticles;
    private Animator potionAnim;
    private AudioSource potionAudioSource;

    private void Start()
    {
        potionAudioSource = GetComponent<AudioSource>();
        potionAnim = GetComponent<Animator>();
        breakParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            potionAudioSource.Play();
            potionAnim.SetTrigger("Break");
            breakParticles.Play();
            Destroy(this.gameObject, breakParticles.main.duration);
        }
    }
}
