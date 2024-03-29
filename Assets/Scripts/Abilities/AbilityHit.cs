using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

// class for storing information about an ability hit
public class AbilityHit
{
    private GameObject prefab_impact;
    private AbilityColor color;
    private float damage;
    private Vector3 velocity;
    private AbilitySpeed speed;

    public AbilityHit(GameObject prefab_impact, AbilityColor color, float damage, Vector3 velocity)
    {
        this.prefab_impact = prefab_impact;
        this.color = color;
        this.damage = damage;
        this.velocity = velocity;
        this.speed = AbilitySpeed.INSTANT;
    }

    public AbilityHit(GameObject prefab_impact, AbilityColor color, float damage, Vector3 velocity, AbilitySpeed speed)
    {
        this.prefab_impact = prefab_impact;
        this.color = color;
        this.damage = damage;
        this.velocity = velocity;
        this.speed = speed;
    }

    public GameObject GetImpactPrefab()
    {
        return prefab_impact;
    }

    public AbilityColor GetColor()
    {
        return color;
    }

    public float GetDamage()
    {
        return damage;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public AbilitySpeed GetSpeed()
    {
        return speed;
    }

    public enum AbilitySpeed
    {
        INSTANT,
        DELAYED
    }
}
