using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{    
    [SerializeField] private float maxInteractDistance = 2f;
    [SerializeField] private Camera mainCamera;
    public Transform handPos;

    private bool isHolding = false;    

    // public event System.Action canPickUp; UI observer stuff. Let's not get ahead of ourselves.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);


        if (Physics.Raycast(ray, out hit, maxInteractDistance) && hit.collider.transform.tag == "Pickup" && !isHolding)
        {
            Debug.Log("dwdwa");
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.transform.GetComponent<PickupObject>().Interact();
            }
        }

       
    }

    private void FixedUpdate()
    {
        
    }
}
