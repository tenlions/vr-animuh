using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAbility : MonoBehaviour
{
    // the prefab to instantiate when the fireball is cast
    public GameObject fireballPrefab;
    // the transform to spawn and charge the fireball at
    public Transform fireballSpawn;
    // the speed at which the fireball grows
    public float growSpeed = 0.1f;
    // the speed at which the fireball's velocity grows
    public float velocitySpeed = .5f;
    // the velocity at which the fireball is shot
    public float fireballVelocity = 50f;

    private GameObject currentFireball;
    private bool buttonPressed;
    private Timer fireballTimer;

    const float MAX_CHARGE = 1f;
    const float INITIAL_SCALE = .1f;
    const float CHARGE_TIME = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed && currentFireball != null)
        {
            ChargeFireball();
        }

        if (Input.GetMouseButtonDown(0) && !buttonPressed)
        {
            if (currentFireball == null)
            {
                CreateFireball();
                buttonPressed = true;
            }

        }
        else if (Input.GetMouseButtonUp(0) && currentFireball != null && buttonPressed)
        {
            ShootFireball();
            buttonPressed = false;
        }
    }

    private void CreateFireball()
    {
        if (currentFireball != null) return;
        DebuggableScript.DebugLog(this.name, "creating new fireball");


        fireballTimer = new Timer(CHARGE_TIME, this.name);
        fireballTimer.StartTimer();

        currentFireball = GameObject.Instantiate(fireballPrefab, fireballSpawn.position, fireballSpawn.rotation);
        currentFireball.transform.localScale *= INITIAL_SCALE;
        DebuggableScript.DebugLog(this.name, "fireball init scale: " + currentFireball.transform.localScale);
    }

    private void ChargeFireball()
    {
        // make sure we really have a fireball
        if (currentFireball == null) return;

        DebuggableScript.DebugLog(this.name, "charging fireball");

        currentFireball.transform.position = fireballSpawn.position;
        DebuggableScript.DebugLog(this.name, "fireball scale: " + currentFireball.transform.localScale);
        if (currentFireball.transform.localScale.x < MAX_CHARGE)
        {
            currentFireball.transform.localScale += Vector3.one * (growSpeed * Time.deltaTime);
        }
        
    }

    private void ShootFireball()
    {
        // only shoot if we have a fireball
        if (currentFireball == null) return;
        DebuggableScript.DebugLog(this.name, "shooting fireball");

        Rigidbody fireballRigidbody = currentFireball.GetComponent<Rigidbody>();

        DebuggableScript.DebugLog(this.name, "fireball direction: " + -fireballSpawn.right);
        fireballRigidbody.velocity = (-fireballSpawn.right * fireballVelocity);

        fireballTimer = null;
        currentFireball = null;
    }
}
