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
    private Vector3 spawnPos; // Where the potion will respawn after getting broken
    private MeshRenderer potionRender;
    private PlayerPotionEffects player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerPotionEffects>();
        potionRender = GetComponent<MeshRenderer>();
        spawnPos = transform.position;
        breakParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void Drink()
    {
        StartCoroutine(DrinkPotion());        
    }

    private IEnumerator DrinkPotion() // This is alllllll hacked TODO
    {
        breakParticles.Play();
        potionRender.enabled = false;
        this.enabled = false; // This will throw a red error
        player.DrinkPotion(this);
        yield return new WaitForSeconds(breakParticles.main.duration);
        InstantiateSelf();
        Destroy(this);
    }

    private void OnDestroy()
    {

    }

    private void InstantiateSelf()
    {
        Instantiate(this, spawnPos, Quaternion.identity);
    }
}
