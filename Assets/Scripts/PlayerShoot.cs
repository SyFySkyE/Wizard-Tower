using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("What the player shoots")]
    [SerializeField] private GameObject bullet;

    [Header("Where the player shoots it")]
    [SerializeField] private Transform gunLocation;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, gunLocation.position, gunLocation.rotation);
        }
    }
}
