using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player's Current HP")]
    [SerializeField] private int healthPoints = 3;

    [Header("How many seconds before player becomes vulnerable after getting hurt")]
    [SerializeField] private float secondsBeforeVulnerableAgain = 3f;

    [Header("How many seconds before scene reloads after player death")]
    [SerializeField] private float secondsBeforeSceneReload = 3f;
     
    private bool isVulnerable = true;
    private Animator playerAnim;
    private bool isDead = false;

    public event System.Action OnHurt;
    public event System.Action OnCameraDeath;
    public event System.Action OnDeath;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

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
        if (healthPoints <= 0 && !isDead)
        {
            PlayerDeath();
        }
        else
        {
            OnHurt();
            playerAnim.SetTrigger("Hurt");
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
        playerAnim.SetTrigger("Death");
        isDead = true;
        OnCameraDeath();
        StartCoroutine(CallDeathEvent());        
    }

    private IEnumerator CallDeathEvent()
    {
        yield return new WaitForSeconds(secondsBeforeSceneReload);
        OnDeath();
    }
}
