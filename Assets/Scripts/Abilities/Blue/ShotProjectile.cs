using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotProjectile : MonoBehaviour
{
    public GameObject hitPrefab;
    private float speed = 10f;
    private bool isFired = false;
    private Vector3 direction;
    const float TIME_STEP = 0.3f;

    // Update is called once per frame
    void Update()
    {
        if(isFired)
        {
            MoveProjectile();
        }      
    }

    void FireProjectile(Vector3 direction)
    {
        isFired = true;
        this.direction = direction;
        Destroy(this, 20f);
    }

    private void MoveProjectile()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Target"))
        {
            GameObject hit = Instantiate(hitPrefab, transform.position, Quaternion.FromToRotation(Vector3.forward, direction));
            Destroy(hit, 2.5f);
            Destroy(gameObject);
        }
    }
}