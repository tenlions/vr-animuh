using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    // the color of the ability
    public AbilityColor GetColor();

    // the damage of the ability
    public float GetDamage();

    // the power cost of the ability
    public float GetPowerCost();

    // the cooldown of the ability in seconds
    public float GetCooldown();

    // starts casting/charging the ability
    public void Cast();

    // charges the ability
    public void Charge();

    // releases/shoots the ability
    public void Release();

    // returns the prefab of the ability
    public GameObject GetAbilityPrefab();

    // returns the current object
    public GameObject GetCurrentObject();

    // whether the ability is currently charging
    public bool IsCharging();
}
