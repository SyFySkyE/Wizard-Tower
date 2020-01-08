using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{    
    [Header("How close does the player need to be interact with objects, in meters")]
    [SerializeField] private float maxInteractDistance = 2f;
    [Header("The player's childed \"Hand Pos\" that pickup Obj's follow")]
    public Transform handPos;

    private Camera mainCamera;
    private PickupObject boxObj;

    private bool isHolding = false;    

    public event System.Action OnCanPickUp;
    public event System.Action OnCanDrop;
    public event System.Action OnNoContext; // No object close enough to show contextual UI tip

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, maxInteractDistance) && hit.collider.transform.tag == "Pickup" && !isHolding)
        {
            OnCanPickUp();
            if (Input.GetKeyDown(KeyCode.E))
            {
                boxObj = hit.collider.transform.GetComponent<PickupObject>();
                boxObj.Interact();
                isHolding = true;
            }
        }
        else if (isHolding && boxObj)
        {
            OnCanDrop();
            if (Input.GetKeyDown(KeyCode.E))
            {
                boxObj.Interact();
                boxObj = null;
                isHolding = false;
            }
        }
        else
        {
            OnNoContext();
        }
    }

    private void FixedUpdate()
    {
        
    }
}
