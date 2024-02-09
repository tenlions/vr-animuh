using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour, IAbility
{
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

    public GameObject GetAbilityPrefab()
    {
        throw new System.NotImplementedException();
    }

    public AbilityColor GetColor()
    {
        return AbilityColor.Blue;
    }

    public float GetCooldown()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetCurrentObject()
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

    public bool IsCharging()
    {
        throw new System.NotImplementedException();
    }
}
