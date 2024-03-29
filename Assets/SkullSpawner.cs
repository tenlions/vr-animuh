using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpawner : MonoBehaviour
{
    [SerializeField]
    private const float EJECT_FORCE = 8f;
    [SerializeField]
    private const float SPAWN_INTERVAL = 5f;
    [SerializeField]
    private const float START_COOLDOWN = 2f;
    private float currentCooldown;
    private Vector3 vec3_skullSpawn;
    public GameObject prefab_skull;

    // Start is called before the first frame update
    void Start()
    {
        currentCooldown = START_COOLDOWN;
        vec3_skullSpawn = transform.position + new Vector3(0, .3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown <= 0)
        {
            // spawn a skull and launch it in a random direction
            Vector3 launchDir = GetLaunchDirection();
            float launchForce = Random.Range(7f, 10f);
            GameObject skull = Instantiate(prefab_skull, vec3_skullSpawn, Quaternion.identity);
            Rigidbody skullRb = skull.GetComponent<Rigidbody>();
            skullRb.AddForce(launchDir * launchForce, ForceMode.Impulse);
            skullRb.MoveRotation(Quaternion.LookRotation(launchDir));
            // reset the cooldown
            currentCooldown = SPAWN_INTERVAL;
        }

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            return;
        }
    }

    private Vector3 GetLaunchDirection()
    {
        Vector2 cirlePoint = Random.insideUnitCircle * 0.5f;
        return new Vector3(cirlePoint.x, 1, cirlePoint.y);
    }
}
