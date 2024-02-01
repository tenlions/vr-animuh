using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Substitution : MonoBehaviour, IAbility
{

    // the target to switch places with
    public GameObject obj_target;
    // the player
    public GameObject obj_player;
    // the VFX to play when the ability is released
    public GameObject vfx_smokePoof;
    // how much to move both subjects of the substitution upwards when the ability is released
    public float upwardDisplacement = 1f;

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
        // save the positions of the player and the target
        Vector3 playerPos = obj_player.transform.position;
        Vector3 targetPos = obj_target.transform.position;

        // switch the positions of the player and the target
        obj_player.transform.position = new Vector3(targetPos.x, targetPos.y + upwardDisplacement, targetPos.z);
        obj_target.transform.position = new Vector3(playerPos.x, playerPos.y + upwardDisplacement, playerPos.z);

        // play the smoke poof VFX
        PlaySmokePoof(playerPos);
        PlaySmokePoof(targetPos);
    }

    // play the smoke poof VFX at the given position and destroy it after 3 seconds
    private void PlaySmokePoof(Vector3 pos)
    {
        GameObject poof = Instantiate(vfx_smokePoof, pos, Quaternion.identity);
        Destroy(poof, 3f);
    }

    public GameObject GetAbilityPrefab()
    {
        throw new System.NotImplementedException();
    }

    public AbilityColor GetColor()
    {
        return AbilityColor.Red;
    }

    public float GetCooldown()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetCurrentObject()
    {
        return obj_target;
    }

    public float GetDamage()
    {
        return 0f;
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
