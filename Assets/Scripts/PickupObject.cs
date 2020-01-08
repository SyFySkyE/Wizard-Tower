using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private PlayerInteract player;

    private bool isBeingHeld = false;
    private Rigidbody boxRb;

    // Start is called before the first frame update
    void Start()
    {
        boxRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            StayInPlayerHand();
        }
        else
        {
            Drop();
        }
    }

    private void Drop()
    {
        boxRb.velocity = Vector3.zero;
        boxRb.useGravity = true;
        boxRb.constraints = RigidbodyConstraints.None;
    }

    private void StayInPlayerHand()
    {
        transform.position = player.handPos.position;
        boxRb.useGravity = false;
        boxRb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Interact()
    {
        isBeingHeld = !isBeingHeld;
    }
}
