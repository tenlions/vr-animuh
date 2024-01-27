using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : ScriptableObject
{
    private float maxScale;

    public Scaler(float maxScale)
    {
        this.maxScale = maxScale;
    }

    public Scaler()
    {
        this.maxScale = 1.0f;
    }

    // scales a given transform in all directions based on the percentage of time elapsed in a given timer
    // the transform will be scaled to maxScale at the end of the timer
    // scales in all directions equally
    public void Scale(Transform target, Timer timer)
    {
        Scale(target, timer, Vector3.one);
    }

    // scales a given transform in the given direction, based on the percentage of time elapsed in a given timer
    // the transform will be scaled to maxScale at the end of the timer
    public void Scale(Transform target, Timer timer, Vector3 direction)
    {
        float percentage = timer.GetPercentage();
        Vector3 scale = Vector3.Normalize(direction) * (percentage * maxScale);
        target.localScale = Vector3.Scale(target.localScale, scale);
    }

    public static Vector3 GetScale(Transform target, Timer timer, float maxScale, Vector3 direction)
    {
        float percentage = timer.GetPercentage();
        Vector3 scale = Vector3.Normalize(direction) * (percentage * maxScale);
        return Vector3.Scale(target.localScale, scale);
    }

    public static Vector3 GetScale(Transform target, Timer timer, float maxScale)
    {
        return GetScale(target, timer, maxScale, Vector3.one);
    }
}