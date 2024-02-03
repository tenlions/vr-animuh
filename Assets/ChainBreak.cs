using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBreak : MonoBehaviour
{

    public AudioSource chainBreakSound;

    private void OnJointBreak(float breakForce)
    {
        chainBreakSound.Play();
    }
}
