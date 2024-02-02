using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PunchieHit : MonoBehaviour
{
    public AudioClip[] hitSounds;
    private AudioClip hitSound;
    public AudioSource punchieSound;
    public float velocityNeed = 1f;
    public float hitVolume = 10f;

    // Start is called before the first frame update
    void Start()
    {
        punchieSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if(collision.relativeVelocity.magnitude > velocityNeed)
        {
            float audioLevel = collision.relativeVelocity.magnitude / (100/hitVolume);

            hitSound = hitSounds[Random.Range(0, hitSounds.Length)];
            punchieSound.clip = hitSound;
            punchieSound.PlayOneShot(punchieSound.clip,audioLevel);
        }
        

        Debug.Log("Punchie hit");
    }
}
