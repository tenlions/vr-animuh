using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public GameObject hitPrefab;
    private float speed = 10f;
    private bool isFired = false;
    private Vector3 direction;

    private Vector3 previousPosition;
    private float lastTime = 0;
    const float timeStep = 0.3f;

    // Update is called once per frame
    void Update()
    {
        if(isFired)
        {
            MoveProjectile();
        }
        if (Time.time > (lastTime + timeStep)) {
            previousPosition = transform.position;
            lastTime = Time.time;
        }
        
    }

    void FireProjectile(Vector3 direction)
    {
        isFired = true;
        //this.direction = Vector3.Normalize(direction);
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
            Vector3 currPos = transform.position;
            Vector3 prevPos = previousPosition;
            Debug.Log("currPos: " + currPos);
            Debug.Log("prevPos: " + prevPos);
            Debug.DrawLine(prevPos, currPos * 3, Color.yellow, 10f);
            GameObject hit = Instantiate(hitPrefab, transform.position, Quaternion.FromToRotation(Vector3.forward, direction));
            Destroy(hit, 2.5f);
            Destroy(this.gameObject);
                    
        }
    }
}
