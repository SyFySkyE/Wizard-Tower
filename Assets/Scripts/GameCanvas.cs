using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI centerText;

    [Header("Contextual text to show player")]
    [SerializeField] private string pickupText = "Press E to Pick Up";
    [SerializeField] private string dropText = "Press E to Drop;";
    [SerializeField] private string interactText = "Press E to Interact";
    [SerializeField] private string drinkText = "Press E to Drink";

    private PlayerInteract player;
    private PlayerHealth playerHeath;
    private Animator canvasAnim;
    private UnityEngine.UI.Image fpDot;

    private void OnEnable()
    {
        fpDot = GetComponentInChildren<UnityEngine.UI.Image>();
        player = FindObjectOfType<PlayerInteract>();
        playerHeath = FindObjectOfType<PlayerHealth>();
        player.OnPotionSplash += Player_OnPotionSplash;
        playerHeath.OnHurt += PlayerHeath_OnHurt;
        playerHeath.OnCameraDeath += PlayerHeath_OnCameraDeath;
        player.OnCanPickUp += Player_canPickUp;
        player.OnCanDrop += Player_OnCanDrop;
        player.OnNoContext += Player_OnNoContext;
        player.OnCanInteract += Player_OnCanInteract;
        player.OnRead += Player_OnRead;
        player.OnCanDrink += Player_OnCanDrink;
        player.OnDrink += Player_OnDrink;
    }

    private void Player_OnPotionSplash()
    {
        canvasAnim.SetTrigger("Splash");
    }

    private void PlayerHeath_OnCameraDeath()
    {
        canvasAnim.SetTrigger("Death");
    }

    private void PlayerHeath_OnHurt()
    {
        canvasAnim.SetTrigger("Hurt");
    }

    private void Player_OnDrink()
    {
        canvasAnim.SetTrigger("Drink");
    }

    private void Player_OnCanDrink()
    {
        centerText.text = drinkText;
    }

    private void Player_OnCanInteract()
    {
        centerText.text = interactText;
    }

    private void Player_OnRead(string obj)
    {
        centerText.text = obj;
    }

    private void Player_OnNoContext()
    {
        centerText.text = "";
    }

    private void Player_OnCanDrop()
    {
        centerText.text = dropText;
    }

    private void Player_canPickUp()
    {
        centerText.text = pickupText;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasAnim = GetComponent<Animator>();
    }

    public void CutSceneRestrictions()
    {
        fpDot.enabled = false;
    }
}
