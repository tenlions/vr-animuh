using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DTO for the state of a hand at a given time
public struct HandState
{
    public HandState(HandPose pose, HandPosition position, float timeStamp)
    {
        this.pose = pose;
        this.position = position;
        this.timeStamp = timeStamp;
    }
    
    // the pose the hand was in
    public HandPose pose { get; }

    // the position the hand was in
    public HandPosition position { get; }

    // the time the state was recorded
    public float timeStamp { get; }
}
