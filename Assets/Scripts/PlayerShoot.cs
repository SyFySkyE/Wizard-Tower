using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("What the player shoots")]
    [SerializeField] private GameObject bullet;

    [Header("Where the player shoots it")]
    [SerializeField] private Transform gunLocation;

    private Animator playerAnim;
    private bool canFire = true;

    private void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canFire)
        {
            Instantiate(bullet, gunLocation.position, gunLocation.rotation);
            playerAnim.SetTrigger("Fire");
        }
    }

    public void StartCutsceneRestriction()
    {
        canFire = false;
    }

    public void DisableCutsceneRestriction()
    {
        canFire = true;
    }
}
