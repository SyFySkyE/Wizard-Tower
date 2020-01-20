using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public enum PotionType { None, Haste, Invisibility, Invulnerability, Shrink, Enlarge }

    [Header("What effect it will give the player if they drink this potion")]
    [SerializeField] private PotionType potionType; // Defaults to None    
    public PotionType ThisPotionType { get { return this.potionType; } }

    [Header("Audio Clips used when speeding up and when it wears off")]
    [SerializeField] private AudioClip speedUpSfx;
    [SerializeField] private float speedUpSfxVolume = 0.5f;
    [SerializeField] private AudioClip speedDownSfx;
    [SerializeField] private float speedDownSfxVolume = 0.5f;
 
    private ParticleSystem breakParticles; 
    private PlayerPotionEffects player;
    private Animator potionAnimator;
    private AudioSource potionAudioSource;

    private bool isDrank = false;

    // Start is called before the first frame update
    void Start()
    {
        potionAudioSource = GetComponent<AudioSource>();
        potionAnimator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerPotionEffects>();
        breakParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void Drink()
    {
        if (!isDrank)
        {
            AudioSource.PlayClipAtPoint(speedUpSfx, player.transform.position, speedUpSfxVolume);
            breakParticles.Play();
            player.DrinkPotion(this);
            potionAnimator.SetTrigger("Drink");
            this.tag = "Drank";
            isDrank = true;
        }        
    }

    public void CanDrink()
    {
        AudioSource.PlayClipAtPoint(speedDownSfx, player.transform.position, speedDownSfxVolume);
        isDrank = false;
        this.tag = "Potion";
    }
}
