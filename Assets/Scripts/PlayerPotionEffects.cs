using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPotionEffects : MonoBehaviour
{
    [Header("Haste Variables. How drinking the \"Haste\" potion effects gameplay.")]
    [SerializeField] private float hasteSpeedMultiplier = 1.5f;
    private float normalSpeed;

    [Header("Time length of potion effects, in seconds. See tooltip if changing!")]
    [Tooltip("If this value gets changed, then the canvas potion drink screen effect will also need to be set to this new value. Ask Chris if you need help.")]
    [SerializeField] private float potionLength = 5f;

    [Header("Sound Effects when drinking Potion")]
    [SerializeField] private AudioClip speedUpSfx;
    [SerializeField] private float speedUpSfxVolume = 0.5f;
    [SerializeField] private AudioClip speedDownSfx;
    [SerializeField] private float speedDownSfxVolume = 0.5f;

    private AudioSource playerAudio;
    private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController playerMove;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerMove = GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
        normalSpeed = playerMove.movementSettings.ForwardSpeed;
    }

    public void DrinkPotion(Potion potion)
    {        
        StartCoroutine(BeginPotionEffects(potion.ThisPotionType.ToString()));
    }

    private IEnumerator BeginPotionEffects(string potionType)
    {
        switch (potionType)
        {
            case "Haste":
                PlayerUsedHaste();
                break;
            case "Enlarge":
                Debug.Log("Player is meant to enlarge.");
                break;
            case "Shrink":
                Debug.Log("Player is meant to shrink.");
                break;
            case "Invulnerability":
                Debug.Log("Player is supposed invulnerable, but this might not be implemented yet. You can find out quickly by jumping in the fire.");
                break;
            case "Invisibility":
                Debug.Log("Player is almost as invisible as I am to my crush.");
                break;
            case "None":
                Debug.LogWarning("Player drank a potion with a type of \"None\". This potion's type was probably never assigned a type. You can do so in the inspector of said potion object. Ask Chris for more info.");
                break;
            default:
                Debug.LogWarning("Player drank a potion with a type that does not exist!!");
                break;
        }
        yield return new WaitForSeconds(potionLength);
        ResetPlayerStats();
    }

    private void PlayerUsedHaste()
    {
        playerAudio.PlayOneShot(speedUpSfx, speedUpSfxVolume);
        playerMove.movementSettings.ForwardSpeed *= hasteSpeedMultiplier;
    }

    private void ResetPlayerStats() // Reset player back to normal
    {
        playerAudio.PlayOneShot(speedDownSfx, speedDownSfxVolume);
        playerMove.movementSettings.ForwardSpeed = normalSpeed;
    }
}
