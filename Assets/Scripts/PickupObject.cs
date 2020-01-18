using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private bool isBeingHeld = false;
    private Rigidbody boxRb;
    private PlayerInteract player;

    public event System.Action OnForceDrop; // If player drags object through boundaries

    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<Rigidbody>())
        {
            this.gameObject.AddComponent<Rigidbody>();
        }
        boxRb = GetComponent<Rigidbody>(); // Lazy load
        
        player = FindObjectOfType<PlayerInteract>();
        this.tag = "Pickup";
        this.gameObject.layer = 9; // Pick
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            StayInPlayerHand();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse == Vector3.zero && collision.relativeVelocity == Vector3.zero) // Object is being dragged inside other objects
        {
            OnForceDrop?.Invoke();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // TODO Don't do it like this, but use this as a base for writing depenetration systems

        //BoxCollider thisCollider = GetComponent<BoxCollider>();
        //Vector3 outV3Info;
        //float moreOutInfo;
        //Physics.ComputePenetration(thisCollider, transform.position, transform.rotation, collision.collider, collision.gameObject.transform.position, collision.gameObject.transform.rotation, out outV3Info, out moreOutInfo);
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
