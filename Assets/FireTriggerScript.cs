using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sending message on trigger enter");
        other.SendMessageUpwards("BlueFireExposure", SendMessageOptions.DontRequireReceiver);
    }
}