using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    [Header("What correct object goes here")]
    [SerializeField] private GameObject correctObjTrigger;

    public event System.Action OnPuzzleComplete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == correctObjTrigger)
        {
            OnPuzzleComplete();
        }
    }
}
