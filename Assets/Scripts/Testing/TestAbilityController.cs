using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbilityController : MonoBehaviour
{

    public IAbility ability_Key1;

    // Start is called before the first frame update
    void Start()
    {
        ability_Key1 = GetComponentInChildren<Substitution>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && ability_Key1 != null)
        {
            ability_Key1.Release();
        }
    }
}
