using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAudio : MonoBehaviour
{
    private AudioSource audioSource;

    public float destoryTime;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            audioSource.Play();
            Destroy(gameObject,destoryTime);
        }
    }
}
