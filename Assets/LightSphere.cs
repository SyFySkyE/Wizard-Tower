using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSphere : MonoBehaviour
{
    [Header("How fast Light Sphere travels")]
    [SerializeField] private float speed = 5f;

    private float secondsBeforeDestroy = 10f; // This is a failsafe in case nothing destroys it.

    private Rigidbody bulletRb;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, secondsBeforeDestroy);
    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, .1f);
        //bulletRb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void Update()
    {
        Ray cameraRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        Physics.Raycast(cameraRay, out hitInfo, Mathf.Infinity);
        transform.position = Vector3.MoveTowards(transform.position, hitInfo.point, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
