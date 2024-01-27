using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHittable : MonoBehaviour, IHittable
{
    public void Hit(AbilityHit hit)
    {
        GetComponent<Rigidbody>().AddForce(hit.GetVelocity(), ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}