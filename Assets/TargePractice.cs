using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPractice : MonoBehaviour
{
    public ParticleSystem targetHitParticle;
    public AudioSource targetHitSound;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("target hit");

        targetHitParticle.Play();

        targetHitSound.Play();

    }
}
