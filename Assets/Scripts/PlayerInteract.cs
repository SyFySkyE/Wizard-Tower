using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("How close does the player need to be interact with objects, in meters")]
    [SerializeField] private float maxInteractDistance = 2f;

    [Header("The player's childed \"Hand Pos\" that pickup Obj's follow")]
    public Transform handPos;

    private Camera mainCamera;
    private PickupObject boxObj;

    private enum PlayerState { isHolding, isReading, None }
    private PlayerState currentState = PlayerState.None;

    bool objForceDrop = false; // TODO tech debt

    public event System.Action OnCanPickUp;
    public event System.Action OnCanDrop;
    public event System.Action OnCanInteract;
    public event System.Action OnCanDrink;
    public event System.Action<string> OnRead;
    public event System.Action OnDrink;
    public event System.Action OnNoContext; // No object close enough to show contextual UI tip

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {  
        CastRay();
    }

    private void CastRay()
    {        
        RaycastHit hit; // Used to store what the ray hits
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward); // Casts a ray from the forward center of camera

        switch (currentState) // Depends on what state player is currently in
        {
            case PlayerState.None:
                if (Physics.Raycast(ray, out hit, maxInteractDistance)) // When the casted out ray with limited distance var (maxInteractDis) hits something
                {
                    switch (hit.collider.transform.tag) // What the ray is hitting. ie, what the player is looking at within Interact distance
                    {
                        case "Pickup":
                            PlayerLookAtPickup(hit);
                            break;
                        case "Read Obj":
                            PlayerLookAtReadable();
                            break;
                        case "Potion":
                            PlayerLookAtPotion(hit);
                            break;
                        default:
                            OnNoContext(); // Event blanks out contextual hit text. Ie, clears the "Press E to Interact" canvas text
                            break;
                    }
                }
                else
                {
                    OnNoContext();
                }
                break;            

            case PlayerState.isReading:
                if (Physics.Raycast(ray, out hit, maxInteractDistance))
                {
                    PlayerIsReading(hit);
                }
                else
                {
                    currentState = PlayerState.None;
                    OnNoContext();
                }
                break;

            case PlayerState.isHolding:
                PlayerHoldingObject();
                break;
        }     
    }

    private void PlayerIsReading(RaycastHit hit)
    {
        ReadObject readObject = hit.collider.transform.GetComponent<ReadObject>(); // Gets component so canvas can get the description from the obj
        if (readObject) // Null check
        {
            OnRead(readObject.ReturnReadObjDescription()); // Canvas receives this event with the str containing obj description
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentState = PlayerState.None; // Player is currently reading, presses E, Player stops reading
        }
    }

    private void PlayerLookAtReadable()
    {
        OnCanInteract(); // Event that GameCanvas receives and sends "Press E to Interact" to screen
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentState = PlayerState.isReading;
        }        
    }

    private void PlayerLookAtPickup(RaycastHit hit)
    {
        OnCanPickUp(); // Event thta GameCanvas receives and sends "Press E to Pickup" to screen
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            boxObj = hit.collider.transform.GetComponent<PickupObject>();
            boxObj.Interact();
            currentState = PlayerState.isHolding;
            boxObj.OnForceDrop += BoxObj_OnForceDrop;
        }
    }

    private void BoxObj_OnForceDrop()
    {
        objForceDrop = true;
    }

    private void PlayerLookAtPotion(RaycastHit hit)
    {
        OnCanDrink();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Potion potionToDrink = hit.collider.transform.GetComponent<Potion>();
            if (potionToDrink) // Null Check
            {
                potionToDrink.Drink();
                OnDrink();
            }            
        }
    }

    private void PlayerHoldingObject()
    {
        OnCanDrop();
        if (Input.GetKeyDown(KeyCode.E) || objForceDrop) // Player is holding obj, presses E, drops it. Or if obj is dragged through colliders
        {
            boxObj.Interact();
            boxObj = null;
            currentState = PlayerState.None;
            objForceDrop = false;
        }
    }
}
