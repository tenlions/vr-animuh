using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAbilityHandler : MonoBehaviour, IAbilityHandler
{
    public float projectileGrowSpeed = 0.1f;

    private const float TIME_CHARGE = 3f;
    private const float SCALE_INITIAL = .01f;
    private const float SCALE_MAX = SCALE_INITIAL * 10;

    public Punch ability_punch;
    public Shot ability_shot;

    public GameObject prefab_projectile;
    public GameObject obj_projectileSpawn;
    private GameObject obj_currentProjectile;
    public GameObject obj_aimPoint;

    private bool triggerHeld;

    private float time_triggerDown;
    private float time_triggerUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerHeld)
        {
            ChargeProjectile();
        }
    }

    public AbilityColor GetColor()
    {
        return AbilityColor.Blue;
    }

    public void HandleTriggerDown(HandState handState, HandState otherHandState)
    {
        triggerHeld = true;
        time_triggerDown = Time.time;
        CreateProjectile();
    }

    public void HandleTriggerUp(HandState handState, HandState otherHandState)
    {
        triggerHeld = false;
        time_triggerUp = Time.time;

        if (HandPose.AIM == otherHandState.pose && HandPosition.AIM == otherHandState.position)
        {
            ShootProjectile();
        }
        else
        {
            Destroy(obj_currentProjectile);
        }
    }

    public void HandleCollision(Collider other)
    {
        if (obj_currentProjectile == null) return;

        IHittable hittable = other.GetComponent<IHittable>();
        if (hittable == null) return;

        ability_punch.Hit(hittable);
    }

    private void CreateProjectile()
    {
        if (obj_currentProjectile != null) return;

        obj_currentProjectile = GameObject.Instantiate(prefab_projectile, obj_projectileSpawn.transform.position, obj_projectileSpawn.transform.rotation);
        obj_currentProjectile.transform.localScale *= SCALE_INITIAL;
    }

    private void ChargeProjectile()
    {
        if (obj_currentProjectile == null) return;

        obj_currentProjectile.transform.position = obj_projectileSpawn.transform.position;
        if (obj_currentProjectile.transform.localScale.x < SCALE_MAX)
        {
            obj_currentProjectile.transform.localScale += Vector3.one * (projectileGrowSpeed * Time.deltaTime);
        }

    }

    private void ShootProjectile()
    {
        if (obj_currentProjectile == null) return;

        Vector3 shotDirection = obj_aimPoint.transform.position - obj_currentProjectile.transform.position;
        shotDirection.Normalize();

        obj_currentProjectile.transform.rotation = Quaternion.identity;
        obj_currentProjectile.SendMessage("FireProjectile", shotDirection, SendMessageOptions.DontRequireReceiver);

        Destroy(obj_currentProjectile, 20f);
        obj_currentProjectile = null;
    }
}
