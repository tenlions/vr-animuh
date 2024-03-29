using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface for objects that can be hit by abilities
public interface IHittable
{
    // called when the object is hit by an ability
    public void TakeHit(AbilityHit hit);
}
