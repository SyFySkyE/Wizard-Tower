using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativePotion : MonoBehaviour
{
    [Header("Break SFX")]
    [SerializeField] private AudioClip breakSfx;
    [SerializeField] private float breakSfxVolume = 0.5f;

    private ParticleSystem breakParticles;
    private Animator potionAnim;
    private AudioSource potionAudioSource;

    private bool wasShot = false;

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
            potionAudioSource.Stop();
            AudioSource.PlayClipAtPoint(breakSfx, transform.position, breakSfxVolume);
            potionAnim.SetTrigger("Break");
            wasShot = true;
            breakParticles.Play();
            Destroy(this.gameObject, breakParticles.main.duration);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && wasShot)
        {
            other.GetComponent<PlayerInteract>().SplashPotion();
            wasShot = false;
        }
    }
}
