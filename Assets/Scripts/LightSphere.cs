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

    private Animator lightSphereAnim;
    private bool isDying = false;
    private Vector3 endPoint;

    private void Start()
    {
        lightSphereAnim = GetComponent<Animator>();
        RaycastHit hitInfo = CastRay();

        endPoint = hitInfo.point; // The light ball travels to whever the ray hits

        StartCoroutine(DestroySelfAfterTimer()); // In case something awful happens and the light ball never hits anything so there wouldn't be any lingering game objects.
    }

    private IEnumerator DestroySelfAfterTimer()
    {
        yield return new WaitForSeconds(secondsBeforeDestroy);
        lightSphereAnim.SetTrigger("Death");
    }

    private RaycastHit CastRay()
    {
        Ray cameraRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(cameraRay, out hitInfo, Mathf.Infinity))
        {
            transform.position = Vector3.MoveTowards(transform.position, hitInfo.point, speed * Time.deltaTime); // TODO I have no idea why this is here
        }

        return hitInfo;
    }

    private void Update()
    {
        Vector3 lastPos = transform.position;
        if (endPoint != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
            if (lastPos == transform.position && !isDying) // Fixes bug where if player spams multiple light spheres, the endpoint can be set to a position of a previous light sphere, so a light sphere would travel to the empty space and stay there. The !isDying check is made so this only happens once.
            {
                lightSphereAnim.SetTrigger("Death");
            }
        }
        else
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World); // If the endPoint returns null, just fire forward
        }
    }    

    private void OnCollisionEnter(Collision collision)
    {
        lightSphereAnim.SetTrigger("Death");
    }

    public void Death()
    {
        isDying = true;
        AudioSource.PlayClipAtPoint(onLightDestroySfx, transform.position, onLightDestroySfxVolume);
        Destroy(this.gameObject);
    }
}
