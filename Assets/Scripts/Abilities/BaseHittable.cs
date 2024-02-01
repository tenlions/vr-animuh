using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base class for objects that can be hit by abilities
public class BaseHittable : MonoBehaviour, IHittable
{
    public void Hit(AbilityHit hit)
    {
    }
}
