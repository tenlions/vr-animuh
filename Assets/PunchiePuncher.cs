using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchiePuncher : MonoBehaviour
{
    public AudioSource punchieSound;
    public AudioClip[] hitSounds;
    private AudioClip hitSound;
    public float velocityNeed = 1f;
    public float MaxVolume = 10f;

    // Start is called before the first frame update
    void Start()
    {
        punchieSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > velocityNeed)
        {
            hitSound = hitSounds[Random.Range(0,hitSounds.Length)];
            punchieSound.clip = hitSound;
            punchieSound.PlayOneShot(punchieSound.clip, collision.relativeVelocity.magnitude / (100/MaxVolume));

        }
    }

}
