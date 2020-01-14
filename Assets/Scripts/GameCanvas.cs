﻿using TMPro;
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

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerInteract>();
        player.OnCanPickUp += Player_canPickUp;
        player.OnCanDrop += Player_OnCanDrop;
        player.OnNoContext += Player_OnNoContext;
        player.OnCanInteract += Player_OnCanInteract;
        player.OnRead += Player_OnRead;
        player.OnCanDrink += Player_OnCanDrink;
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
