using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public Light lightSource;

    public float flickerOffset = 0.5f;
    public float flickerThreshold = 95f;
    private float lastFlicker = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (((Time.time - lastFlicker) > flickerOffset) && (Random.Range(0f, 100f) >= 95f))
        {
            lastFlicker = Time.time;
            lightSource.enabled = false;
            Invoke("EnableLight", Random.Range(0.1f, 1f));
        }
    }

    private void EnableLight()
    {
        lightSource.enabled = true;
    }
}
