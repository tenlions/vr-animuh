using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFireLightFlicker : MonoBehaviour
{
    public Light localLight;
    public float maxInterval = 1f;
    float targetIntensity;
    float lastIntensity;
    float interval;
    float timer;
    public float maxDisplacement = 0.25f;
    Vector3 targetPosition;
    Vector3 lastPosition;
    Vector3 origin;

    private void Start()
    {
        localLight = this.gameObject.GetComponent<Light>();
        origin = transform.localPosition;
        lastPosition = origin;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            lastIntensity = localLight.intensity;
            targetIntensity = Random.Range(0.5f, 1f);
            timer = 0;
            interval = Random.Range(0, maxInterval);
            targetPosition = origin + Random.insideUnitSphere * maxDisplacement;
            lastPosition = localLight.transform.localPosition;
        }
        localLight.intensity = Mathf.Lerp(lastIntensity, targetIntensity, timer / interval);
        localLight.transform.localPosition = Vector3.Lerp(lastPosition, targetPosition, timer / interval);
    }
}
