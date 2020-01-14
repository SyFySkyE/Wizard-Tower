using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private enum PotionType { None, Haste, Invisibility, Invulnerability, Shrink, Enlarge }

    [Header("What effect it will give the player if they drink this potion")]
    [SerializeField] private PotionType potionType; // Defaults to None

    private ParticleSystem breakParticles; 
    private Vector3 spawnPos; // Where the potion will respawn after getting broken

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        breakParticles = GetComponent<ParticleSystem>();
    }

    public void Drink()
    {
        StartCoroutine(DrinkPotion());
    }

    private IEnumerator DrinkPotion()
    {
        breakParticles.Play();
        yield return new WaitForSeconds(breakParticles.main.duration);
        InstantiateSelf();
        Destroy(this);
    }

    private void InstantiateSelf()
    {
        Instantiate(this, spawnPos, Quaternion.identity);
    }
}
