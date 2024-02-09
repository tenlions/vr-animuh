using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public Vector3 axis = Vector3.up;
    public Vector3 edge;
    public Vector3 lookTarget;
    public float degreesPerSecond = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(edge, axis, degreesPerSecond * Time.deltaTime);
        transform.LookAt(lookTarget);
    }
}
