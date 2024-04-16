using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundPlayer : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            source.Play();
        }
    }
}
