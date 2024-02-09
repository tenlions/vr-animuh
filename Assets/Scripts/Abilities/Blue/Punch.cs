using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Punch : MonoBehaviour, IAbility 
{

    // the prefab to instantiate for the ability, e.g. the the projectile for a fireball
    public GameObject prefab_ability;
    // the transform to spawn and charge the ability at
    public Transform transform_spawn;
    // the maximum scale of the ability prefab
    public float scaleMax;
    // the initial scale of the ability prefab
    public float scaleInitial;
    // the time it takes to fully charge the ability
    public float chargeTime;
    // the speed at which the ability grows
    public float growSpeed;
    // the time step for the ability
    public float timeStep = 0;
    const float FACTOR_IMPACT = 50f;

    // the current ability being charged
    private GameObject obj_current;
    
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
        if (obj_current != null) return;
        
        obj_current = Instantiate(prefab_ability, transform_spawn.position, transform_spawn.rotation);
    }

    public void Charge()
    {
        if (obj_current == null) return;

        obj_current.transform.position = transform_spawn.position;
        if (obj_current.transform.localScale.x < scaleMax)
        {
            obj_current.transform.localScale += Vector3.one * (growSpeed * Time.deltaTime);
        }
    }

    public void Release()
    {
        if (obj_current == null) return;

        obj_current = null;
    }

    public void Hit(IHittable hittable)
    {
        if (obj_current == null) return;

        Vector3 velocity = GetComponentInParent<HandController>().hand.GetTrackedObjectVelocity().normalized;
        hittable.TakeHit(new AbilityHit(obj_current, GetColor(), 0, velocity * FACTOR_IMPACT));
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
        return prefab_ability;
    }

    public GameObject GetCurrentObject()
    {
        return obj_current;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (obj_current == null) return;

        Vector3 velocity = GetComponentInParent<HandController>().hand.GetTrackedObjectVelocity().normalized;
        other.GetComponent<IHittable>()?.TakeHit(new AbilityHit(obj_current, GetColor(), 0, velocity * FACTOR_IMPACT));

        Debug.DrawLine(velocity, velocity * 3, Color.yellow, 10f);
    }

    public bool IsCharging()
    {
        return obj_current != null;
    }
}
