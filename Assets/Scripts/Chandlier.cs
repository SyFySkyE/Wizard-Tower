using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandlier : MonoBehaviour
{
    private ParticleSystem[] candles;
    private bool isLit = false;

    // Start is called before the first frame update
    void Start()
    {
        candles = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isLit)
        {
            foreach (ParticleSystem ps in candles)
            {
                ps.Play();
                isLit = true;
            }
        }
    }
}
