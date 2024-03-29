using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Punch : MonoBehaviour, IAbility 
{
    // the time step for the ability
    public float timeStep = 0;
    const float FACTOR_IMPACT = 20f;
    public GameObject prefab_impact;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cast()
    {
        throw new System.NotImplementedException();
    }

    public void Charge()
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }

    public void Hit(IHittable hittable)
    {
        Vector3 velocity = GetComponentInParent<HandController>().hand.GetTrackedObjectVelocity().normalized;
        hittable.TakeHit(new AbilityHit(prefab_impact, GetColor(), 0, velocity * FACTOR_IMPACT));
    }

    public AbilityColor GetColor()
    {
        return AbilityColor.Blue;

    }

    public float GetCooldown()
    {
        throw new System.NotImplementedException();
    }

    public float GetDamage()
    {
        throw new System.NotImplementedException();
    }

    public float GetPowerCost()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetAbilityPrefab()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetCurrentObject()
    {
        throw new System.NotImplementedException();
    }

    public bool IsCharging()
    {
        throw new System.NotImplementedException();
    }
}
