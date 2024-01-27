using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Timer : ScriptableObject
{
    // the time in seconds this timer will run for
    protected float maxTime;
    // the time this timer started at
    protected float startTime;
    // the name of the component this timer is for
    protected string componentName;
    // a list of game objects and the message to send them when the timer is finished
    protected Dictionary<GameObject, string> messageTargets = new();

    private bool started = false;

    public Timer(float max, string component)
    {
        this.maxTime = max;
        this.componentName = component;
        DebuggableScript.DebugLog(componentName, "created new timer with max " + maxTime);
    }

    public Timer(float max, string component, Dictionary<GameObject, string> messageTargets)
    {
        this.maxTime = max;
        this.componentName = component;
        this.messageTargets.AddRange(messageTargets);
        DebuggableScript.DebugLog(componentName, "created new timer with max " + maxTime);
    }

    public void StartTimer()
    {
        startTime = Time.time;
        started = true;
        DebuggableScript.DebugLog(componentName, "timer started at " + startTime);
    }

    public void EndTimer()
    {
        started = false;
        MessageTargets();
        DebuggableScript.DebugLog(componentName, "timer ended at " + Time.time);
    }

    void FixedUpdate()
    {
        if (IsFinished())
        {
            EndTimer();
        }

    }

    public bool IsFinished()
    {
        return started && (GetElapsedTime() >= maxTime);
    }


    // returns the percentage of time elapsed
    public float GetPercentage()
    {
        return GetElapsedTime() / maxTime;
    }

    // returns the time elapsed
    public float GetElapsedTime()
    {
        if (!started) return 0;

        return Time.time - startTime;
    }

    private void MessageTargets()
    {
        foreach (KeyValuePair<GameObject, string> entry in messageTargets)
        {
            DebuggableScript.DebugLog(componentName, "sending message '" + entry.Value + "' to " + entry.Key.name);
            entry.Key.SendMessageUpwards(entry.Value, SendMessageOptions.DontRequireReceiver);
        }
    }
}
