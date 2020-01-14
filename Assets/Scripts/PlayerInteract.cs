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

    public event System.Action OnCanPickUp;
    public event System.Action OnCanDrop;
    public event System.Action OnCanInteract;
    public event System.Action<string> OnRead;
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

        switch (currentState)
        {
            case PlayerState.None:
                if (Physics.Raycast(ray, out hit, maxInteractDistance))
                {
                    switch (hit.collider.transform.tag)
                    {
                        case "Pickup":
                            PlayerLookAtPickup(hit);
                            break;
                        case "Read Obj":
                            PlayerLookAtReadable();
                            break;
                        default:
                            OnNoContext();
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
        ReadObject readObject = hit.collider.transform.GetComponent<ReadObject>();
        if (readObject) // Null check
        {
            OnRead(readObject.ReturnReadObjDescription());
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentState = PlayerState.None;
        }
    }

    private void PlayerLookAtReadable()
    {
        OnCanInteract();
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentState = PlayerState.isReading;
        }        
    }

    private void PlayerLookAtPickup(RaycastHit hit)
    {
        OnCanPickUp();
        if (Input.GetKeyDown(KeyCode.E))
        {
            boxObj = hit.collider.transform.GetComponent<PickupObject>();
            boxObj.Interact();
            currentState = PlayerState.isHolding;
        }
    }

    private void PlayerHoldingObject()
    {
        OnCanDrop();
        if (Input.GetKeyDown(KeyCode.E))
        {
            boxObj.Interact();
            boxObj = null;
            currentState = PlayerState.None;
        }
    }
}
