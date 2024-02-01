using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base interface to be implemented by all enemies
public interface IEnemy
{
    // the enemy's base health
    float GetBaseHealth();

    // the enemy's current health
    float GetCurrentHealth();

    // set the enemy's current health to the given value
    void SetCurrentHealth(float health);

    // make the enemy take the given damage
    void TakeDamage(float damage);

    // make the enemy attack the given target
    void Attack(GameObject target);

    // make the enemy die
    void Die();
}
