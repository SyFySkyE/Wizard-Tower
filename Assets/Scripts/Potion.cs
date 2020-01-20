using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public enum PotionType { None, Haste, Invisibility, Invulnerability, Shrink, Enlarge }

    [Header("What effect it will give the player if they drink this potion")]
    [SerializeField] private PotionType potionType; // Defaults to None    
    public PotionType ThisPotionType { get { return this.potionType; } }    

    private ParticleSystem breakParticles; 
    private PlayerPotionEffects player;
    private Animator potionAnimator;
    private AudioSource drinkSfx;

    private bool isDrank = false;

    // Start is called before the first frame update
    void Start()
    {
        potionAnimator = GetComponent<Animator>();
        drinkSfx = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerPotionEffects>();
        breakParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void Drink()
    {
        if (!isDrank)
        {
            breakParticles.Play();
            player.DrinkPotion(this);
            potionAnimator.SetTrigger("Drink");
            this.tag = "Drank";
            isDrank = true;
            drinkSfx.Play();
        }        
    }

    public void CanDrink()
    {
        isDrank = false;
        this.tag = "Potion";
    }
}
