using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleFlicker : MonoBehaviour
{
    public Light localLight;
    public float maxInterval = 1f;
    public float intensityMin = 5f;
    public float intensityMax = 10f;
    public int rangeFactor = 5;
    public float maxDisplacement = 0.25f;
    float targetValue;
    float lastIntensity;
    float lastRange;
    float interval;
    float timer;
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
            lastRange = localLight.range;

            targetValue = Random.Range(intensityMin, intensityMax);

            timer = 0;
            interval = Random.Range(0, maxInterval);
            targetPosition = origin + Random.insideUnitSphere * maxDisplacement;
            lastPosition = localLight.transform.localPosition;
        }
        localLight.intensity = Mathf.Lerp(lastIntensity, targetValue, timer / interval);
        localLight.range = Mathf.Lerp(lastRange, targetValue * rangeFactor, timer / interval);
        localLight.transform.localPosition = Vector3.Lerp(lastPosition, targetPosition, timer / interval);
    }
}
