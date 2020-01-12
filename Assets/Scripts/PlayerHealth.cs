using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player's Current HP")]
    [SerializeField] private int healthPoints = 3;

    [Header("How many seconds before player becomes vulnerable after getting hurt")]
    [SerializeField] private float secondsBeforeVulnerableAgain = 3f;

    private bool isVulnerable = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (isVulnerable)
        {
            if (collision.gameObject.CompareTag("Harmful"))
            {
                HurtPlayer();                
            }
        }        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (isVulnerable)
        {
            if (other.CompareTag("Harmful"))
            {
                HurtPlayer();
            }
        }        
    }

    private void HurtPlayer()
    {
        healthPoints--;
        if (healthPoints <= 0)
        {
            PlayerDeath();
        }
        else
        {
            StartCoroutine(InvulnerabilityTimer());
        }        
    }

    private IEnumerator InvulnerabilityTimer()
    {        
        isVulnerable = false;
        yield return new WaitForSeconds(secondsBeforeVulnerableAgain);
        isVulnerable = true;
    }

    private void PlayerDeath()
    {
        Debug.Log("Player HP is 0");
    }
}
