using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainbreaker : MonoBehaviour
{
    public AudioSource breakSound;

    private void OnJointBreak(float breakForce)
    {
        breakSound.Play();
    }
}
