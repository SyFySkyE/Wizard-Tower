using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI centerText;

    [Header("Contextual text to show player")]
    [SerializeField] private string pickupText = "Press E to Pick Up";
    [SerializeField] private string dropText = "Press E to drop;";

    private PlayerInteract player;

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerInteract>();
        player.OnCanPickUp += Player_canPickUp;
        player.OnCanDrop += Player_OnCanDrop;
        player.OnNoContext += Player_OnNoContext;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
