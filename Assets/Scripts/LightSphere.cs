using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSphere : MonoBehaviour
{
    [Header("How fast Light Sphere travels")]
    [SerializeField] private float speed = 5f;

    [Header("Sfx that plays when light gets destroyed")]
    [SerializeField] private AudioClip onLightDestroySfx;
    [SerializeField] private float onLightDestroySfxVolume = 0.5f;

    private float secondsBeforeDestroy = 6f; // This is a failsafe in case nothing destroys it.

    private Rigidbody bulletRb; // TODO Remove
    private Animator lightSphereAnim;
    private Vector3 endPoint;

    private void Start()
    {
        lightSphereAnim = GetComponent<Animator>();
        bulletRb = GetComponent<Rigidbody>();
        Ray cameraRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(cameraRay, out hitInfo, Mathf.Infinity))
        {
            transform.position = Vector3.MoveTowards(transform.position, hitInfo.point, speed * Time.deltaTime);
        }

        endPoint = hitInfo.point;
        Destroy(this.gameObject, secondsBeforeDestroy);
    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, .1f);
        //bulletRb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void Update()
    {
        if (endPoint != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
        }        
    }    

    private void OnCollisionEnter(Collision collision)
    {
        lightSphereAnim.SetTrigger("Death");
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(onLightDestroySfx, transform.position, onLightDestroySfxVolume);
    }
}
