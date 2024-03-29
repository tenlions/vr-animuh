using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : MonoBehaviour, IEnemy, IHittable
{
    [SerializeField]
    private const float BASE_HEALTH = 1f;
    [SerializeField]
    private const float MOVE_SPEED = 5f;
    [SerializeField]
    private const float TURN_SPEED = 1f;
    [SerializeField]
    private const float INITIAL_COOLDOWN = .7f;
    [SerializeField]
    private float currentCooldown;
    private float currentHealth;
    private bool wasPunched;
    private Transform transform_player;
    private Rigidbody rigidbody_self;
    public GameObject prefab_deathEffect;

    

    // Start is called before the first frame update
    void Start()
    {
        wasPunched = false;
        currentHealth = BASE_HEALTH;
        transform_player = GameObject.Find("Player").transform;
        rigidbody_self = GetComponent<Rigidbody>();
        currentCooldown = INITIAL_COOLDOWN;
    }

    // Update is called once per frame
    void Update()
    {

        if ((transform.position - transform_player.position).magnitude <= .5f) 
        {
            Die();
        }

        // if we were punched, do nothing
        if (wasPunched) return;

        // wait for the initial cooldown to end
        if (currentCooldown > 0f)
        {   
            currentCooldown -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (currentCooldown <= 0f && !wasPunched)
        {
            // look at and move towards the player
            rigidbody_self.useGravity = false;
            Quaternion rotationTo = Quaternion.LookRotation(transform_player.position - transform.position);
            Quaternion rotation = Quaternion.Lerp(transform.rotation, rotationTo, TURN_SPEED * Time.deltaTime);
            rigidbody_self.MoveRotation(rotation);
            rigidbody_self.MovePosition(transform.position + (MOVE_SPEED * Time.deltaTime * transform.forward));
            //rigidbody_self.Move(transform.position + (MOVE_SPEED * Time.deltaTime * transform.forward), Quaternion.LookRotation(transform_player.position - transform.position));
        }
    }

    public void Attack(GameObject target)
    {
        return;
    }

    public void Die()
    {
        PlayDeathEffect();
        Destroy(gameObject);
    }

    public float GetBaseHealth()
    {
        return BASE_HEALTH;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(float health)
    {
        currentHealth = health;

        if (currentHealth <= 0) Die();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) Die();
    }

    public float GetMoveSpeed()
    {
        return MOVE_SPEED;
    }

    private void PlayDeathEffect()
    {
        GameObject deathEffect = Instantiate(prefab_deathEffect, transform.position, Quaternion.identity);
        deathEffect.transform.localScale *= .2f;
    }

    public void TakeHit(AbilityHit hit)
    {
        wasPunched = true;
        rigidbody_self.useGravity = true;
        rigidbody_self.AddForce(hit.GetVelocity(), ForceMode.Impulse);
        if (hit.GetSpeed() == AbilityHit.AbilitySpeed.INSTANT)
        {
            Die();
        } 
        else
        {
            Invoke("Die", 5f);
        }
    }
}
