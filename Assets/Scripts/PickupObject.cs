using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private bool isBeingHeld = false;
    private Rigidbody boxRb;
    private PlayerInteract player;

    // Start is called before the first frame update
    void Start()
    {
        boxRb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            StayInPlayerHand();
        }
    }

    private void Drop()
    {
        boxRb.velocity = Vector3.zero;
        boxRb.useGravity = true;
        boxRb.constraints = RigidbodyConstraints.None;
        isBeingHeld = false;
    }

    private void StayInPlayerHand()
    {
        transform.position = player.handPos.position;
        boxRb.useGravity = false;
        boxRb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Interact()
    {
        if (!isBeingHeld)
        {
            Pickup();
        }
        else
        {
            Drop();
        }
    }

    public void Pickup()
    {
        isBeingHeld = true;
    }
}
